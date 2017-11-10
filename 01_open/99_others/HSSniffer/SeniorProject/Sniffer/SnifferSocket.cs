using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
//using System.Diagnostics;
using System.Collections;

namespace Sniffer
{
	public delegate void IPv4DatagramCallback(IPv4Datagram datagram);
	public delegate void IPv4FragmentCallback(IPv4Fragment fragment);
	public delegate void SnifferErrorCallback(SnifferException e);
	public delegate void ConnectionCallback(IPEndPoint source, IPEndPoint destination);

	/// <summary>
	/// Summary description for SnifferSocket.
	/// </summary>
	public class SnifferSocket
	{
		private Hashtable     SocketMap_  = null;
		private DataManager   Data_       = null;
		private string IP="";
		private bool paused=false;

		public ArrayList IPs=null;
		public event IPv4DatagramCallback IPv4DatagramReceived;
		public event IPv4FragmentCallback IPv4FragmentReceived;
		public event SnifferErrorCallback SnifferError;
		
		// Creates a new SnifferSocket.
		public SnifferSocket() { 
			Data_ = new DataManager();
			SocketMap_ = new Hashtable();
			IPs=new ArrayList();
		}

		public bool Pause 
		{ 
			get { return paused; }
			set { paused = value; }
		}


		// This will start the Sniffer so that it will listen the IP passed in.
		// All of the Sniffing is done asynchronously so this method will return immediately.  
		public void Sniff(String ip) 
		{
			IP=ip;
			Socket socket = new Socket(AddressFamily.InterNetwork,SocketType.Raw,ProtocolType.IP);
			byte[] buffer = new byte[2048];
			SocketPair socketpair = new SocketPair(socket,buffer);
			socket.Blocking = true;
			try
			{
				socket.Bind(new IPEndPoint(IPAddress.Parse(ip),0));
			}
			catch(SocketException e)
			{
				throw new SnifferException("Cannot assign requested address.The requested address is not valid in its context.",e);
			}
			this.SetupSocket(socket);
			
			if ( SocketMap_.Contains(ip) ) 
			{ 
				throw new SnifferException("Socket already bound on that IP");
			} 
			else 
			{ 
				SocketMap_.Add(ip,socketpair);
			}
			try 
			{ 
				socket.BeginReceive(buffer,0,buffer.Length,SocketFlags.None,new AsyncCallback(this.ReceivePacket),socketpair);
				paused=false;
			} 
			catch ( Exception e ) 
			{ 
				throw new SnifferException("Could not start the Receive",e);
			}
		}


		//set up Socket to recieve all.
		private void SetupSocket(Socket socket) { 
			bool ret_val = true;
			SocketException e = null;
			try { 
				socket.SetSocketOption(SocketOptionLevel.IP,SocketOptionName.HeaderIncluded,1);
				byte []IN = new byte[4]{1, 0, 0, 0};
				byte []OUT = new byte[4];
				int SIO_RCVALL = unchecked((int)0x98000001);
				int ret_code = socket.IOControl(SIO_RCVALL, IN, OUT);
				ret_code = OUT[0] + OUT[1] + OUT[2] + OUT[3];
				if(ret_code != 0)
					ret_val = false;
			} catch ( SocketException ex ) { 
				e = ex;
				ret_val = false;
			} finally { 
				if ( ! ret_val ) {
					throw new SnifferException("Could not set the socket to receive all",e);
				}
			}

		}

		public void resumeSocket()
		{
			SocketPair p = SocketMap_[IP] as SocketPair;
			Socket socket = p.IPSocket;
			bool ret_val = true;
			SocketException e = null;
			try 
			{ 
				socket.BeginReceive(p.Buffer,0,p.Buffer.Length,SocketFlags.None,new AsyncCallback(this.ReceivePacket),p);
				this.paused=false;
			} 
			catch ( SocketException ex ) 
			{ 
				e = ex;
				ret_val = false;
			} 
			finally 
			{ 
				if ( ! ret_val ) 
				{
					throw new SnifferException("Could not resume the socket",e);
				}
			}
		}
		
		// Callback function for the Asynchronous Receive on a Socket.	
		private void ReceivePacket(IAsyncResult ar) 
		{
			bool fired=false;
			int len = 0;
			SocketPair p = ar.AsyncState as SocketPair;
			Socket socket = p.IPSocket;
			int type = 0;
			try 
			{ 
				len = socket.EndReceive(ar);
			} 
			catch ( SocketException e) 
			{ 
				fired = true;
				FireSnifferError(new SnifferException("Error Receiving Packet",e));
			}

			if (!fired)
			{
				type = HeaderParser.ToInt(p.Buffer,0,4);
				try 
				{ 
					switch(type) 
					{ 
						case 4:
							HandleIPv4Datagram(p.Buffer);
							break;
					}
				} 
				catch ( Exception e ) 
				{
					FireSnifferError(new SnifferException(e.Message.ToString(),e));
				}
			}
			if (!this.paused)
			{
				socket.BeginReceive(p.Buffer,0,p.Buffer.Length,SocketFlags.None,new AsyncCallback(this.ReceivePacket),p);
			}
		}

		// Parses out an IPv4 Datagram.  
		private void HandleIPv4Datagram(byte[] buffer) { 
			int identification = 0;
			int protocol = 0;
			uint source = 0;
			uint dest   = 0;
			int header_length = 0;
			IPv4Datagram datagram = null;
			IPAddress source_ip;
			IPAddress dest_ip;


			source = HeaderParser.ToUInt(buffer,96,32);
			dest = HeaderParser.ToUInt(buffer,128,32);
			source_ip = IPAddress.Parse(IPv4Datagram.GetIPString(source));
			dest_ip = IPAddress.Parse(IPv4Datagram.GetIPString(dest));
			if (isRelatedToThisCom(source_ip.ToString(),dest_ip.ToString()))
			{

				IPv4Fragment fragment = new IPv4Fragment();

				fragment.MoreFlag = (HeaderParser.ToByte(buffer,50,1) > 0) ? true : false;
				fragment.Offset = HeaderParser.ToInt(buffer,51,13) * 8;
				fragment.TTL = HeaderParser.ToInt(buffer,64,8);
				fragment.Length  = HeaderParser.ToUShort(buffer,16,16);
				header_length = HeaderParser.ToInt(buffer,4,4) * 4;
				fragment.SetData(buffer,header_length,fragment.Length - header_length);

				identification = HeaderParser.ToInt(buffer,32,16);
				protocol = HeaderParser.ToByte(buffer,72,8);
//				source = HeaderParser.ToUInt(buffer,96,32);
//				dest = HeaderParser.ToUInt(buffer,128,32);
//				source_ip = IPAddress.Parse(IPv4Datagram.GetIPString(source));
//				dest_ip = IPAddress.Parse(IPv4Datagram.GetIPString(dest));
			
				datagram = Data_.GetIPv4Datagram(identification,source_ip,dest_ip);

				if ( datagram == null ) 
				{ 
					datagram = new IPv4Datagram();
					datagram.IHL = HeaderParser.ToInt(buffer,4,4) * 4;
					datagram.TypeOfService = HeaderParser.ToInt(buffer,8,8);
					datagram.ReservedFlag = HeaderParser.ToInt(buffer,48,1);
					datagram.DontFragmentFlag = HeaderParser.ToInt(buffer,49,1);
					datagram.Identification = identification;
					datagram.TTL = HeaderParser.ToInt(buffer,64,8);
					datagram.Checksum = HeaderParser.ToInt(buffer,80,16);
					datagram.Source = source_ip;
				
					datagram.SourceName=DnsTable.GetName(source_ip.ToString());

					datagram.DestinationName=DnsTable.GetName(dest_ip.ToString());

					datagram.Destination = dest_ip;
					datagram.Protocol = protocol;
					if (datagram.IHL==24)
						datagram.Options = HeaderParser.ToInt(buffer,160,32);

				}

				datagram.AddFragment(fragment);
				if ( datagram.Complete ) 
				{ 
					datagram.SetPorts();
					if (FilterManager.isAllowed(datagram.GetUpperProtocol(),datagram.SourceIP,datagram.DestinationIP,datagram.SPort,datagram.DPort))
					{
						FireIPv4DatagramReceived(datagram);

						datagram.SetHeader(buffer);
						if ( datagram.WasFragmented() ) 
						{
							Data_.RemoveIPv4Datagram(datagram);
						}
					}
				} 
				else 
				{ 
					Data_.AddIPv4Datagram(datagram);
					this.FireIPv4FragmentReceived(fragment);
				}
			}

		}

		private void FireSnifferError(SnifferException e ) { 
			if ( SnifferError != null ) { 
				SnifferError(e);
			}
		}

		// This will fire a IPv4DatagramReceived event.
		private void FireIPv4DatagramReceived(IPv4Datagram datagram) { 
			if (IPv4DatagramReceived != null ) { 
				IPv4DatagramReceived(datagram);
			}
		}

		// This will fire a IPv4FragmentReceived event
		private void FireIPv4FragmentReceived(IPv4Fragment fragment) { 
			
			if (IPv4FragmentReceived != null ) { 
				IPv4FragmentReceived(fragment);
			}
		}

		private bool isRelatedToThisCom(string srcIp,string destIp)
		{
			return (IPs.Contains(srcIp)||IPs.Contains(destIp));
		}
	}
}
