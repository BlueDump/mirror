using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading;
using System.Collections;
using Sniffer;


namespace SnifferUI
{
	/// <summary>
	/// Summary description for CheckTool.
	/// </summary>
	/// 
	
	public delegate void CheckToolCallback(string str);

	public class CheckTool
	{
		static public EndPoint				thisCmpIp;
		static public int					SendTimeout			= 3000;
		static public int					ReceiveTimeout		= 3000;
		static public int					PingLoopCount		= 4;
		static public int					TraceRouteLoopCount = 30;
		static public string				PingType;
		static public string				TraceRouteType;
		static public string				PortSanProtocolType;
		
		static public ArrayList				portArr = new ArrayList();
		static public IPAddress				host;
		static public AddressFamily			addressFamily;
		static public SocketType			socketType;
		static public ProtocolType			protocolType;
		static public int					port=-1;
		static public bool					isFinished=false;
		
		static public CheckToolCallback	checkToolCallback;
				
		static CheckTool()
		{
			IPEndPoint ipend=new IPEndPoint(Dns.GetHostByName(Dns.GetHostName()).AddressList[0],0);
			thisCmpIp=(EndPoint)ipend;
			portArr.Add(53);
			portArr.Add(80);
			portArr.Add(139);
		}

		

		public static void Ping()
		{	
			PingType = Setting.PingType;
			TraceRouteType = Setting.TraceRouteType;
			PortSanProtocolType = Setting.PortSanProtocolType;
			checkToolCallback("Ping request to host "+DnsTable.GetName(host.ToString())+" ["+host.ToString()+"]");
			if (PingType!="Udp")
			{
				addressFamily = AddressFamily.InterNetwork;
				socketType    = SocketType.Raw;
				protocolType  = ProtocolType.Icmp;
			}
			else
			{
				addressFamily = AddressFamily.InterNetwork;
				socketType    = SocketType.Dgram;
				protocolType  = ProtocolType.Udp;
			}

			port = 0;
			int recvPacketCount=0;
			int lostPacketCount=0;
			int percent = 0;
			Socket socket = new Socket(addressFamily,socketType,protocolType);
			socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout,SendTimeout); 
			socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout,ReceiveTimeout);
			
			IPEndPoint iep = new IPEndPoint( host, port );
			EndPoint ep = (EndPoint)iep;

			if (protocolType==ProtocolType.Icmp)
			{
				Icmp icmp = new Icmp();

				if (PingType=="Echo Request")
				{
					icmp.Type = 8;
				}
				if (PingType=="Time Stamp")
				{
					icmp.Type = 13;
				}
				icmp.Code     = 0;
				icmp.DataSize = 0;
			
				byte[] data;	

				if ( icmp.Type ==8 )	// gerisi sonra yapilacak
				{				
					//Initialize data
					char[] c = {'a','b','c','d','e','f','g','h','i','j','k','l',
								   'm','n','o','p','q','r','s','t','u','v','w','a',
								   'b','c','d','e','f','g','h','i'};

					data= new byte[c.Length+4];
					for (int i = 0; i < c.Length; i++)
					{
						data[i+4] = (byte)c[i];
					}

					icmp.DataSize = data.Length;
					Buffer.BlockCopy(BitConverter.GetBytes(2), 0, data, 0, 2);
					Buffer.BlockCopy(BitConverter.GetBytes(0), 0, data, 2, 2);
				
					Buffer.BlockCopy(data, 0, icmp.Data, 0, data.Length);
					int seq;
					for ( seq=0; seq < PingLoopCount ; seq++)
					{
						icmp.Checksum=0;
						Buffer.BlockCopy(BitConverter.GetBytes(seq), 0, icmp.Data, 2, 2);
						UInt16 chcksum = icmp.getChecksum();
						icmp.Checksum = chcksum;
						byte[] buf = icmp.getBytes();
						DateTime dt= DateTime.Now;
				
						socket.SendTo(buf, 4+icmp.DataSize, SocketFlags.None, iep);
						TimeSpan ts;
						try
						{
							buf = new byte[1024];
							int recv = socket.ReceiveFrom(buf,ref ep);
							recvPacketCount++;
							ts   = DateTime.Now- dt;
							checkToolCallback("reply from: "+DnsTable.GetName(iep.Address.ToString())+" [" + iep.Address.ToString() + "], seq: " + seq +
								", time = " + ts.Milliseconds + "ms");
						} 
						catch (SocketException)
						{
							lostPacketCount++;
							checkToolCallback("No reply from host...");
						}
				
						Thread.Sleep(1000);
					}

					percent = (lostPacketCount/seq)*100;
					checkToolCallback("Send packet : "+seq.ToString()+" Received packet : "+recvPacketCount.ToString()+" Lost packet : "+lostPacketCount.ToString() + " (%"+percent.ToString()+")");
				}

				if ( icmp.Type ==13 )	// gerisi sonra yapilacak
				{				
					data= new byte[4+12];
					icmp.DataSize = data.Length;

					Buffer.BlockCopy(BitConverter.GetBytes(2), 0, data, 0, 2);				
				
					Buffer.BlockCopy(BitConverter.GetBytes(0), 0, data, 8, 4);
					Buffer.BlockCopy(BitConverter.GetBytes(0), 0, data, 12, 4);
				
					Buffer.BlockCopy(data, 0, icmp.Data, 0, data.Length);
					int seq;
					for (seq=0; seq < PingLoopCount ; seq++)
					{					
						Buffer.BlockCopy(BitConverter.GetBytes(seq+1), 0, icmp.Data, 2, 2);
						Buffer.BlockCopy(BitConverter.GetBytes(DateTime.Now.Millisecond), 0, icmp.Data, 4, 4);
						icmp.Checksum=0;
						UInt16 chcksum = icmp.getChecksum();
						icmp.Checksum = chcksum;
						byte[] buf = icmp.getBytes();
						DateTime dt= DateTime.Now;
				
						socket.SendTo(buf, 4+icmp.DataSize, SocketFlags.None, iep);
						TimeSpan ts;
						try
						{
							buf = new byte[1024];
							int recv = socket.ReceiveFrom(buf,ref ep);
							recvPacketCount++;
							ts   = DateTime.Now- dt;
							checkToolCallback("reply from: "+DnsTable.GetName(iep.Address.ToString())+" [" + iep.Address.ToString() + "], seq: " + seq +
								", time = " + ts.Milliseconds + "ms");
						} 
						catch (SocketException)
						{
							lostPacketCount++;
							checkToolCallback("No reply from host...");
						}				
				
						Thread.Sleep(1000);
					}
					percent = (lostPacketCount/seq)*100;
					checkToolCallback("Send packet : "+seq.ToString()+" Received packet : "+recvPacketCount.ToString()+" Lost packet : "+lostPacketCount.ToString() + " (%"+percent.ToString()+")");
				}
			}
			if ( protocolType ==ProtocolType.Udp )	// gerisi sonra yapilacak
			{
				IPHostEntry hostEntry = Dns.Resolve(Dns.GetHostName());
				IPEndPoint endPoint = new IPEndPoint(hostEntry.AddressList[0], 11000);

				char[] c = {'a','b','c','d','e','f','g','h','i','j','k','l',
							   'm','n','o','p','q','r','s','t','u','v','w','a',
							   'b','c','d','e','f','g','h','i'};

				byte[] data= new byte[c.Length];
				for (int i = 0; i < c.Length; i++)
				{
					data[i] = (byte)c[i];
				}

				

				Socket tSocket = new Socket(AddressFamily.InterNetwork,SocketType.Raw,ProtocolType.Icmp);
				tSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout,SendTimeout); 
				tSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout,ReceiveTimeout);
				tSocket.Bind(endPoint);
				byte[] buf;
				int seq;
				for (seq=0; seq < PingLoopCount ; seq++)
				{					
					DateTime dt= DateTime.Now;				
					socket.SendTo(data,data.Length , SocketFlags.None, iep);
					TimeSpan ts;
					
					try
					{
						buf = new byte[1024];
						int recv = tSocket.ReceiveFrom(buf,ref ep);
						recvPacketCount++;
						ts   = DateTime.Now- dt;
						checkToolCallback("reply from: "+DnsTable.GetName(iep.Address.ToString())+" [" + iep.Address.ToString() + "], seq: " + seq +
							", time = " + ts.Milliseconds + "ms");
					} 
					catch (SocketException)
					{
						lostPacketCount++;
						checkToolCallback("No reply from host...");
					}				
				
					Thread.Sleep(1000);
				}
				tSocket.Close();
				percent = (lostPacketCount/seq)*100;
				checkToolCallback("Send packet : "+seq.ToString()+" Received packet : "+recvPacketCount.ToString()+" Lost packet : "+lostPacketCount.ToString() + " (%"+percent.ToString()+")");
			}
		
			socket.Close();
			
			
		}

		public static void TraceRoute()
		{
			PingType = Setting.PingType;
			TraceRouteType = Setting.TraceRouteType;
			PortSanProtocolType = Setting.PortSanProtocolType;
			int hop=1;
			checkToolCallback("Trace route to host "+DnsTable.GetName(host.ToString())+" ["+host.ToString()+"]\nFor a maximum of 30 hops");

			if (TraceRouteType!="Udp")
			{
				addressFamily = AddressFamily.InterNetwork;
				socketType    = SocketType.Raw;
				protocolType  = ProtocolType.Icmp;
			}
			else
			{
				addressFamily = AddressFamily.InterNetwork;
				socketType    = SocketType.Dgram;
				protocolType  = ProtocolType.Udp;
			}

			port = 0;
			
			Socket socket = new Socket(addressFamily,socketType,protocolType);
			socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout,SendTimeout); 
			socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout,ReceiveTimeout);
			
			IPEndPoint iep = new IPEndPoint( host, port );
			EndPoint ep = (EndPoint)iep;

			if (protocolType==ProtocolType.Icmp)
			{
				Icmp icmp = new Icmp();

				if (TraceRouteType=="Echo Request")
				{
					icmp.Type = 8;
				}
				if (TraceRouteType=="Time Stamp")
				{
					icmp.Type = 13;
				}
				icmp.Code     = 0;
				icmp.DataSize = 0;
			
				byte[] data;	

				if ( icmp.Type ==8 )	// gerisi sonra yapilacak
				{				
					//Initialize data
					char[] c = {'a','b','c','d','e','f','g','h','i','j','k','l',
								   'm','n','o','p','q','r','s','t','u','v','w','a',
								   'b','c','d','e','f','g','h','i'};

					data= new byte[c.Length+4];
					for (int i = 0; i < c.Length; i++)
					{
						data[i+4] = (byte)c[i];
					}

					icmp.DataSize = data.Length;
					Buffer.BlockCopy(BitConverter.GetBytes(2), 0, data, 0, 2);
					Buffer.BlockCopy(BitConverter.GetBytes(0), 0, data, 2, 2);
				
					Buffer.BlockCopy(data, 0, icmp.Data, 0, data.Length);
					int cik = 0;
					for (int seq=0; seq < TraceRouteLoopCount ; seq++)
					{
						socket.SetSocketOption(SocketOptionLevel.IP,SocketOptionName.IpTimeToLive, seq+1);
						icmp.Checksum=0;
						Buffer.BlockCopy(BitConverter.GetBytes(seq), 0, icmp.Data, 2, 2);
						UInt16 chcksum = icmp.getChecksum();
						icmp.Checksum = chcksum;
						byte[] buf = icmp.getBytes();
						DateTime dt= DateTime.Now;
				
						socket.SendTo(buf, 4+icmp.DataSize, SocketFlags.None, iep);
						TimeSpan ts;
						try
						{
							buf = new byte[1024];
							int recv = socket.ReceiveFrom(buf,ref ep);
							ts   = DateTime.Now- dt;

							Icmp response = new Icmp(buf, recv);

							if (response.Type == 11)
							{
								checkToolCallback(hop+" : response from "+DnsTable.GetName(ep.ToString().Substring(0,ep.ToString().IndexOf(":")))
									+" ["+ep.ToString().Substring(0,ep.ToString().IndexOf(":"))+"] , "+(ts.Milliseconds)+" ms");
								hop++;
							}
							if (response.Type == 0)
							{
								checkToolCallback(DnsTable.GetName(ep.ToString().Substring(0,ep.ToString().IndexOf(":")))
									+" ["+ep.ToString().Substring(0,ep.ToString().IndexOf(":"))+"] reached in "+hop+" hop, "+(ts.Milliseconds)+" ms.");							
								break;
							}
							cik = 0;
						} 
						catch (SocketException)
						{							
							checkToolCallback(hop+" : Request time out...");
							hop++;
							cik++;
							if (cik == 5)
							{
								checkToolCallback("Unable to contact remote host");
								break;
							}
						}				
				
						Thread.Sleep(1000);
					}
				}

				if ( icmp.Type ==13 )	// gerisi sonra yapilacak
				{				
					data= new byte[4+12];
					icmp.DataSize = data.Length;

					Buffer.BlockCopy(BitConverter.GetBytes(2), 0, data, 0, 2);				
				
					Buffer.BlockCopy(BitConverter.GetBytes(0), 0, data, 8, 4);
					Buffer.BlockCopy(BitConverter.GetBytes(0), 0, data, 12, 4);
				
					Buffer.BlockCopy(data, 0, icmp.Data, 0, data.Length);
					int cik = 0;
					for (int seq=0; seq < TraceRouteLoopCount ; seq++)
					{
						socket.SetSocketOption(SocketOptionLevel.IP,SocketOptionName.IpTimeToLive, seq+1);
						Buffer.BlockCopy(BitConverter.GetBytes(seq+1), 0, icmp.Data, 2, 2);
						Buffer.BlockCopy(BitConverter.GetBytes(DateTime.Now.Millisecond), 0, icmp.Data, 4, 4);
						icmp.Checksum=0;
						UInt16 chcksum = icmp.getChecksum();
						icmp.Checksum = chcksum;
						byte[] buf = icmp.getBytes();
						DateTime dt= DateTime.Now;
				
						socket.SendTo(buf, 4+icmp.DataSize, SocketFlags.None, iep);
						TimeSpan ts;
						try
						{
							buf = new byte[1024];
							int recv = socket.ReceiveFrom(buf,ref ep);
							ts   = DateTime.Now- dt;
							
							Icmp response = new Icmp(buf, recv);

							if (response.Type == 11)
							{
								checkToolCallback(hop+" : Response from "+DnsTable.GetName(ep.ToString().Substring(0,ep.ToString().IndexOf(":")))
									+" ["+ep.ToString().Substring(0,ep.ToString().IndexOf(":"))+"] , "+(ts.Milliseconds)+" ms");
								hop++;
							}
							if (response.Type == 14)
							{
								checkToolCallback(DnsTable.GetName(ep.ToString().Substring(0,ep.ToString().IndexOf(":")))
									+" ["+ep.ToString().Substring(0,ep.ToString().IndexOf(":"))+"] reached in "+hop+" hop, "+(ts.Milliseconds)+" ms.");							
								break;
							}
							cik = 0;
						} 
						catch (SocketException)
						{
							checkToolCallback(hop+" : Request time out...");
							cik++;
							hop++;
							if (cik == 5)
							{
								checkToolCallback("Unable to contact remote host");
								break;
							}
						}				
				
						Thread.Sleep(1000);
					}
				}
			}
			if ( protocolType ==ProtocolType.Udp )	// gerisi sonra yapilacak
			{
				IPHostEntry hostEntry = Dns.Resolve(Dns.GetHostName());
				IPEndPoint endPoint = new IPEndPoint(hostEntry.AddressList[0], 11000);

				char[] c = {'a','b','c','d','e','f','g','h','i','j','k','l',
							   'm','n','o','p','q','r','s','t','u','v','w','a',
							   'b','c','d','e','f','g','h','i'};

				byte[] data= new byte[c.Length];
				for (int i = 0; i < c.Length; i++)
				{
					data[i] = (byte)c[i];
				}

				

				Socket tSocket = new Socket(AddressFamily.InterNetwork,SocketType.Raw,ProtocolType.Icmp);
				tSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout,SendTimeout); 
				tSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout,ReceiveTimeout);
				tSocket.Bind(endPoint);
				int cik = 0;
				byte[] buf;
				for (int seq=0; seq < TraceRouteLoopCount ; seq++)
				{
					socket.SetSocketOption(SocketOptionLevel.IP,SocketOptionName.IpTimeToLive, seq+1);
					DateTime dt= DateTime.Now;				
					socket.SendTo(data,data.Length , SocketFlags.None, iep);
					TimeSpan ts;
					
					try
					{
						buf = new byte[1024];
						int recv = tSocket.ReceiveFrom(buf,ref ep);
						ts   = DateTime.Now- dt;

						Icmp response = new Icmp(buf, recv);

						if (response.Type == 11)
						{
							checkToolCallback(hop+" : response from "+DnsTable.GetName(ep.ToString().Substring(0,ep.ToString().IndexOf(":")))
								+" ["+ep.ToString().Substring(0,ep.ToString().IndexOf(":"))+"] , "+(ts.Milliseconds)+" ms");
							hop++;
						}
						if (response.Type == 3)
						{
							checkToolCallback(DnsTable.GetName(ep.ToString().Substring(0,ep.ToString().IndexOf(":")))
								+" ["+ep.ToString().Substring(0,ep.ToString().IndexOf(":"))+"] reached in "+hop+" hop, "+(ts.Milliseconds)+" ms.");							
							break;
						}
						cik = 0;
					} 
					catch (SocketException)
					{
						checkToolCallback(hop+" : Request time out...");
						cik++;
						hop++;
						if (cik == 5)
						{
							checkToolCallback("Unable to contact remote host");
							break;
						}
					}				
				
					Thread.Sleep(1000);
				}
				tSocket.Close();
			}
			
			socket.Close();
			checkToolCallback("Trace complete");
		}

		public static void Check()
		{

			PingType = Setting.PingType;
			TraceRouteType = Setting.TraceRouteType;
			PortSanProtocolType = Setting.PortSanProtocolType;
			bool reached=false;
			bool portAccessed=false;
			TimeSpan lastts;

			checkToolCallback("Checking path to host "+DnsTable.GetName(host.ToString())+" ["+host.ToString()+"]");
			string ret = null;
			if (TraceRouteType!="Udp")
			{
				addressFamily = AddressFamily.InterNetwork;
				socketType    = SocketType.Raw;
				protocolType  = ProtocolType.Icmp;
			}
			else
			{
				addressFamily = AddressFamily.InterNetwork;
				socketType    = SocketType.Dgram;
				protocolType  = ProtocolType.Udp;
			}

			if (!portArr.Contains(port)&& port!=-1)
				portArr.Add(port);
			
			Socket socket = new Socket(addressFamily,socketType,protocolType);
			socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout,SendTimeout); 
			socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout,ReceiveTimeout);
			
			IPEndPoint iep = new IPEndPoint( host, port );
			EndPoint ep = (EndPoint)iep;
			EndPoint eplast = thisCmpIp;
			EndPoint epfirst = ep;

			if (protocolType==ProtocolType.Icmp)
			{
				Icmp icmp = new Icmp();

				if (TraceRouteType=="Echo Request")
				{
					icmp.Type = 8;
				}
				if (TraceRouteType=="Time Stamp")
				{
					icmp.Type = 13;
				}
				icmp.Code     = 0;
				icmp.DataSize = 0;
			
				byte[] data;	

				if ( icmp.Type ==8 )	// gerisi sonra yapilacak
				{				
					//Initialize data
					char[] c = {'a','b','c','d','e','f','g','h','i','j','k','l',
								   'm','n','o','p','q','r','s','t','u','v','w','a',
								   'b','c','d','e','f','g','h','i'};

					data= new byte[c.Length+4];
					for (int i = 0; i < c.Length; i++)
					{
						data[i+4] = (byte)c[i];
					}

					icmp.DataSize = data.Length;
					Buffer.BlockCopy(BitConverter.GetBytes(2), 0, data, 0, 2);
					Buffer.BlockCopy(BitConverter.GetBytes(0), 0, data, 2, 2);
				
					Buffer.BlockCopy(data, 0, icmp.Data, 0, data.Length);
					int cik = 0;
					lastts = DateTime.Now - DateTime.Now;
					lastts -=lastts;
					for (int seq=0; seq < TraceRouteLoopCount ; seq++)
					{
						socket.SetSocketOption(SocketOptionLevel.IP,SocketOptionName.IpTimeToLive, seq+1);
						icmp.Checksum=0;
						Buffer.BlockCopy(BitConverter.GetBytes(seq), 0, icmp.Data, 2, 2);
						UInt16 chcksum = icmp.getChecksum();
						icmp.Checksum = chcksum;
						byte[] buf = icmp.getBytes();
						DateTime dt= DateTime.Now;
				
						socket.SendTo(buf, 4+icmp.DataSize, SocketFlags.None, iep);
						TimeSpan ts;
						try
						{
							buf = new byte[1024];
							int recv = socket.ReceiveFrom(buf,ref ep);
							ts   = DateTime.Now- dt;
							if(ts.Milliseconds<lastts.Milliseconds)
								ts=lastts;

							Icmp response = new Icmp(buf, recv);

							if (response.Type == 11)
							{
								ret += "From "+DnsTable.GetName(eplast.ToString().Substring(0,eplast.ToString().IndexOf(":")))
											+" ["+eplast.ToString().Substring(0,eplast.ToString().IndexOf(":"))+"] to "
											+DnsTable.GetName(ep.ToString().Substring(0,ep.ToString().IndexOf(":")))
											+" ["+ep.ToString().Substring(0,ep.ToString().IndexOf(":"))+"] reached in "+(ts.Milliseconds-lastts.Milliseconds)+" ms\n";
//								checkToolCallback("hop "+seq+" : response from "+ep.ToString()+" , "+(ts.Milliseconds)+" ms");
							}
							if (response.Type == 0)
							{
								reached=true;
								ret += "From "+DnsTable.GetName(eplast.ToString().Substring(0,eplast.ToString().IndexOf(":")))
											+" ["+eplast.ToString().Substring(0,eplast.ToString().IndexOf(":"))+"] to "
											+DnsTable.GetName(ep.ToString().Substring(0,ep.ToString().IndexOf(":")))
											+" ["+ep.ToString().Substring(0,ep.ToString().IndexOf(":"))+"] reached in "+(ts.Milliseconds-lastts.Milliseconds)+" ms\n";
//								checkToolCallback(ep.ToString()+" reached in "+seq+" hops, "+(ts.Milliseconds)+" ms.");							
								break;
							}
							cik = 0;
							lastts=ts;
						} 
						catch (SocketException)
						{							
							//checkToolCallback(e.ErrorCode.ToString()+" "+e.Message/*"No reply from host..."*/);
							cik++;
							if (cik == 4)
							{
								
								if (ep.ToString().Substring(0,ep.ToString().IndexOf(":")) == epfirst.ToString().Substring(0,epfirst.ToString().IndexOf(":")))
									ret += "No response obtained \n";
								else
									ret += "No response obtained after "+DnsTable.GetName(ep.ToString().Substring(0,ep.ToString().IndexOf(":")))
										+" ["+ep.ToString().Substring(0,ep.ToString().IndexOf(":"))+"]\n";
//								checkToolCallback("Unable to contact remote host");
								break;
							}
						}
						
						eplast=ep;
						Thread.Sleep(1000);
					}
				}

				if ( icmp.Type ==13 )	// gerisi sonra yapilacak
				{				
					data= new byte[4+12];
					icmp.DataSize = data.Length;

					Buffer.BlockCopy(BitConverter.GetBytes(2), 0, data, 0, 2);				
				
					Buffer.BlockCopy(BitConverter.GetBytes(0), 0, data, 8, 4);
					Buffer.BlockCopy(BitConverter.GetBytes(0), 0, data, 12, 4);
				
					Buffer.BlockCopy(data, 0, icmp.Data, 0, data.Length);
					int cik = 0;
					lastts = DateTime.Now - DateTime.Now;;
					lastts -=lastts;

					for (int seq=0; seq < TraceRouteLoopCount ; seq++)
					{
						socket.SetSocketOption(SocketOptionLevel.IP,SocketOptionName.IpTimeToLive, seq+1);
						Buffer.BlockCopy(BitConverter.GetBytes(seq+1), 0, icmp.Data, 2, 2);
						Buffer.BlockCopy(BitConverter.GetBytes(DateTime.Now.Millisecond), 0, icmp.Data, 4, 4);
						icmp.Checksum=0;
						UInt16 chcksum = icmp.getChecksum();
						icmp.Checksum = chcksum;
						byte[] buf = icmp.getBytes();
						DateTime dt= DateTime.Now;
				
						socket.SendTo(buf, 4+icmp.DataSize, SocketFlags.None, iep);
						TimeSpan ts;
						try
						{
							buf = new byte[1024];
							int recv = socket.ReceiveFrom(buf,ref ep);
							ts   = DateTime.Now- dt;
							if(ts.Milliseconds<lastts.Milliseconds)
								ts=lastts;
							
							Icmp response = new Icmp(buf, recv);

							if (response.Type == 11)
							{
								ret += "From "+DnsTable.GetName(eplast.ToString().Substring(0,eplast.ToString().IndexOf(":")))
                                    +" ["+eplast.ToString().Substring(0,eplast.ToString().IndexOf(":"))+"] to "
									+DnsTable.GetName(ep.ToString().Substring(0,ep.ToString().IndexOf(":")))
									+" ["+ep.ToString().Substring(0,ep.ToString().IndexOf(":"))+"] reached in "+(ts.Milliseconds-lastts.Milliseconds)+" ms\n";
							//	checkToolCallback("hop "+seq+" : response from "+ep.ToString()+" , "+(ts.Milliseconds)+" ms");
							}
							if (response.Type == 14)
							{
								reached=true;
								ret += "From "+DnsTable.GetName(eplast.ToString().Substring(0,eplast.ToString().IndexOf(":")))
									+" ["+eplast.ToString().Substring(0,eplast.ToString().IndexOf(":"))+"] to "
									+DnsTable.GetName(ep.ToString().Substring(0,ep.ToString().IndexOf(":")))
									+" ["+ep.ToString().Substring(0,ep.ToString().IndexOf(":"))+"] reached in "+(ts.Milliseconds-lastts.Milliseconds)+" ms\n";
							//	checkToolCallback(ep.ToString()+" reached in "+seq+" hops, "+(ts.Milliseconds)+" ms.");							
								break;
							}
							cik = 0;
							lastts=ts;
						} 
						catch (SocketException)
						{
							//checkToolCallback(e.ErrorCode.ToString()+" "+e.Message/*"No reply from host..."*/);
							cik++;
							if (cik == 4)
							{
								
								if (ep.ToString().Substring(0,ep.ToString().IndexOf(":")) == epfirst.ToString().Substring(0,epfirst.ToString().IndexOf(":")))
									ret += "No response obtained \n";
								else
									ret += "No response obtained after "+DnsTable.GetName(ep.ToString().Substring(0,ep.ToString().IndexOf(":")))
										+" ["+ep.ToString().Substring(0,ep.ToString().IndexOf(":"))+"]\n";
								//checkToolCallback("Unable to contact remote host");
								break;
							}
						}				
						eplast=ep;
						Thread.Sleep(1000);
					}
				}
			}
			if ( protocolType ==ProtocolType.Udp )	// gerisi sonra yapilacak
			{
				IPHostEntry hostEntry = Dns.Resolve(Dns.GetHostName());
				IPEndPoint endPoint = new IPEndPoint(hostEntry.AddressList[0], 11000);

				char[] c = {'a','b','c','d','e','f','g','h','i','j','k','l',
							   'm','n','o','p','q','r','s','t','u','v','w','a',
							   'b','c','d','e','f','g','h','i'};

				byte[] data= new byte[c.Length];
				for (int i = 0; i < c.Length; i++)
				{
					data[i] = (byte)c[i];
				}

				

				Socket tSocket = new Socket(AddressFamily.InterNetwork,SocketType.Raw,ProtocolType.Icmp);
				tSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout,SendTimeout); 
				tSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout,ReceiveTimeout);
				tSocket.Bind(endPoint);
				int cik = 0;
				byte[] buf;
			
				lastts = DateTime.Now - DateTime.Now;;
				lastts -=lastts;

				for (int seq=0; seq < TraceRouteLoopCount ; seq++)
				{
					socket.SetSocketOption(SocketOptionLevel.IP,SocketOptionName.IpTimeToLive, seq+1);
					DateTime dt= DateTime.Now;				
					socket.SendTo(data,data.Length , SocketFlags.None, iep);
					TimeSpan ts;
					
					try
					{
						buf = new byte[1024];
						int recv = tSocket.ReceiveFrom(buf,ref ep);
						ts   = DateTime.Now- dt;

						if(ts.Milliseconds<lastts.Milliseconds)
							ts=lastts;

						Icmp response = new Icmp(buf, recv);

						if (response.Type == 11)
						{
							ret += "From "+DnsTable.GetName(eplast.ToString().Substring(0,eplast.ToString().IndexOf(":")))
								+" ["+eplast.ToString().Substring(0,eplast.ToString().IndexOf(":"))+"] to "
								+DnsTable.GetName(ep.ToString().Substring(0,ep.ToString().IndexOf(":")))
								+" ["+ep.ToString().Substring(0,ep.ToString().IndexOf(":"))+"] reached in "+(ts.Milliseconds-lastts.Milliseconds)+" ms\n";
							//checkToolCallback("hop "+seq+" : response from "+ep.ToString()+" , "+(ts.Milliseconds)+" ms");
						}
						if (response.Type == 3)
						{
							reached=true;
							ret += "From "+DnsTable.GetName(eplast.ToString().Substring(0,eplast.ToString().IndexOf(":")))
								+" ["+eplast.ToString().Substring(0,eplast.ToString().IndexOf(":"))+"] to "
								+DnsTable.GetName(ep.ToString().Substring(0,ep.ToString().IndexOf(":")))
								+" ["+ep.ToString().Substring(0,ep.ToString().IndexOf(":"))+"] reached in "+(ts.Milliseconds-lastts.Milliseconds)+" ms\n";
							//checkToolCallback(ep.ToString()+" reached in "+seq+" hops, "+(ts.Milliseconds)+" ms.");							
							break;
						}
						cik = 0;
						lastts=ts;
					} 
					catch (SocketException)
					{
						//checkToolCallback(e.ErrorCode.ToString()+" "+e.Message/*"No reply from host..."*/);
						cik++;
						if (cik == 4)
						{
							if (ep.ToString().Substring(0,ep.ToString().IndexOf(":")) == epfirst.ToString().Substring(0,epfirst.ToString().IndexOf(":")))
								ret += "No response obtained \n";
							else
								ret += "No response obtained after "+DnsTable.GetName(ep.ToString().Substring(0,ep.ToString().IndexOf(":")))
									+" ["+ep.ToString().Substring(0,ep.ToString().IndexOf(":"))+"]\n";
							//checkToolCallback("Unable to contact remote host");
							break;
						}
					}				
					eplast = ep;
					Thread.Sleep(1000);
				}
				tSocket.Close();
			}
		
			socket.Close();
			if (!reached)
			{
				
				
				for (int i=0;i<portArr.Count;i++)
				{
					socket = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
					port = (int)portArr[i];
					iep = new IPEndPoint( host, port );
					ep = (EndPoint)iep;

					try
					{
						ret += "Checking port : "+port.ToString()+" on Host : "+DnsTable.GetName(host.ToString())+" ["+host.ToString()+"]\n";
						socket.Connect( ep );
						if ( !socket.Connected )
						{
							ret += "Port "+port.ToString()+" is in CLOSED state \n";
							//checkToolCallback(ret);
						}
						else
						{
							socket.Close();
							ret += "Port "+port.ToString()+" is in LISTENING state \n";
							portAccessed=true;
							break;
							//checkToolCallback(ret);
						}
					}
					catch ( SocketException)
					{
						socket.Close();
						ret += "Port "+port.ToString()+" is in CLOSED state \n";
						//checkToolCallback(ret);
					}
				}
			}

			
			if (!reached && portAccessed)			
				ret += "Failed to trace to host but host reachable...";
			if (!reached && !portAccessed)
				ret += "Failed to trace to host and host unreachable...";
			checkToolCallback(ret);
			isFinished=true;
		}
	}
}
