namespace PortScannerNS
{
	using System;
	using System.Collections;
	using System.Net;
	using System.Net.Sockets;
	using System.Xml;
	using System.Windows.Forms;


//	public delegate void OnPortRespondCallback(string sever , int Number);
//	public delegate void OnPortNotRespondCallback(string sever , int Number);
	
	/////////////////////////////////////////////////////////////////////////
	/// Encapsulates a complete configuration.

	public class Configuration
	{


		public Configuration()
		{
		}

		public Configuration( string configuration_path )
		{
			serviceController = new System.ServiceProcess.ServiceController("SMTPSVC");

			XmlTextReader reader = new XmlTextReader( configuration_path );
			XmlDocument doc = new XmlDocument();
			doc.Load( reader );

			// --

			XmlNode settings_node = doc.SelectSingleNode( "/configuration/settings" );
			Settings = new Settings( settings_node );

			// --

			XmlNodeList server_nodes = doc.SelectNodes( "/configuration/servers/server" );
			ArrayList servers = new ArrayList( server_nodes.Count );

			foreach( XmlNode server_node in server_nodes )
			{
				Server server = new Server( this, server_node );
				servers.Add( server );
				ServerNames.Add(server.Address);
				ServerTable.Add(server.Address,server);
			}

			Servers = (Server[])servers.ToArray( typeof(Server) );

			// --

			doc = null;
			reader = null;
		}

		

		public Settings Settings;

		public Server[] Servers;

		public ArrayList ServerNames = new ArrayList();
		public Hashtable ServerTable = new Hashtable();

		System.ServiceProcess.ServiceController serviceController;
		
	}

	/////////////////////////////////////////////////////////////////////////
	/// In-memory representation of the settings from a config file.

	public class Settings
	{

		public Settings()
		{
		}

		public Settings( XmlNode settings_node )
		{
			XmlAttribute a = settings_node.Attributes["mailServer"];
			if ( a!=null )
				MailServer = Convert.ToString(a.Value);
			else
				throw new ApplicationException(a.Name + " must be specified.");

			a = settings_node.Attributes["mailSenderEMailAddress"];
			if ( a!=null )
				EMailSenderAddress = Convert.ToString(a.Value);
			else
				throw new ApplicationException(a.Name + " must be specified.");

			a = settings_node.Attributes["mailPortNotAvailableSubject"];
			if ( a!=null )
				EMailPortNotAvailableSubject = Convert.ToString(a.Value);
			else
				EMailPortNotAvailableSubject = "";

			a = settings_node.Attributes["loopCount"];
			if ( a!=null )
				LoopCount = Convert.ToInt32(a.Value);
			else
				LoopCount = 0;

			a = settings_node.Attributes["loopIntervalSeconds"];
			if ( a!=null )
				LoopIntervalSeconds = Convert.ToInt32(a.Value);
			else
				LoopIntervalSeconds = 3600;

			a = settings_node.Attributes["waitEndKey"];
			if ( a!=null )
				WaitEndKey = Convert.ToBoolean(a.Value);
			else
				WaitEndKey = false;

			// --

			XmlNode n = settings_node.SelectSingleNode( "descendant::mailNotAvailableBody" );
			if ( n!=null )
			{
				EMailPortNotAvailableBody = n.Value;
				// CDATA-section.
				if ( EMailPortNotAvailableBody==null && n.ChildNodes.Count>0 )
					EMailPortNotAvailableBody = n.ChildNodes[0].Value;
			}
			else
				EMailPortNotAvailableBody = "";

			n = settings_node.SelectSingleNode( "descendant::mailNotAvailableBodyPort" );
			if ( n!=null )
			{
				EMailPortNotAvailableBodyPort = n.Value;
				// CDATA-section.
				if ( EMailPortNotAvailableBodyPort==null && n.ChildNodes.Count>0 )
					EMailPortNotAvailableBodyPort = n.ChildNodes[0].Value;
			}
			else
				EMailPortNotAvailableBodyPort = "";
		}

		public string MailServer;
		public string EMailSenderAddress;
		public string EMailPortNotAvailableSubject;
		public string EMailPortNotAvailableBody;
		public string EMailPortNotAvailableBodyPort;

		public int LoopCount;
		public int LoopIntervalSeconds;
		public bool LoopEndless { get { return LoopCount==0; } }

		public bool WaitEndKey;
	}
 
	/////////////////////////////////////////////////////////////////////////
	/// In-memory representation of ONE server to check.

	public class Server
	{
		public Server()
		{
		}

		public Server( Configuration owner, XmlNode server_node )
		{
			Owner = owner;

			// --

			XmlAttribute a = server_node.Attributes["isActive"];
			if ( a!=null )
				IsActive = Convert.ToBoolean(a.Value);

			a = server_node.Attributes["address"];
			if ( a!=null )
				Address = Convert.ToString(a.Value);

			// --

			XmlNodeList ns = server_node.SelectNodes( "mailReceiverEMailAddresses/address" );

			ArrayList l = new ArrayList( ns.Count );

			foreach ( XmlNode n in ns )
			{
				a = n.Attributes["address"];
				if ( a!=null )
					l.Add( Convert.ToString(a.Value) );
					mailAdressList.Add(a.Value.ToString());
			}

			EMailPortNotAvailableReceivers = (string[])l.ToArray( typeof(string) );
			
			// --

			ns = server_node.SelectNodes( "ports/port" );

			l = new ArrayList( ns.Count );

			foreach ( XmlNode n in ns )
			{
				Port port = new Port( this, n );
				l.Add( port );
				PortTable.Add(((port.HasNumber)?
					port.Number.ToString():
					port.NumberRangeBegin+"-"+port.NumberRangeEnd.ToString()),port);
			}

			Ports = (Port[])l.ToArray( typeof(Port) );
		}

		// --

		public void Process()
		{
			foreach ( Port port in Ports )
			{
				port.Process();
			}

			OnFinishedProcessing();
		}

		public void OnFinishedProcessing()
		{
			// Check for unreachable ports and send e-mail.
			
			if ( UnreachablePorts.Count==0 )
				return;

			string subject = ExpandVariables( Owner.Settings.EMailPortNotAvailableSubject );
			string body = ExpandVariables( Owner.Settings.EMailPortNotAvailableBody );

			// Send to multiple receipients.
//			if (Owner.isMailServiceStarted)
//			{
//				foreach ( string to in EMailPortNotAvailableReceivers )
//				{
//					SmtpClient.Send( 
//						Owner.Settings.MailServer,
//						Owner.Settings.EMailSenderAddress,
//						to,
//						subject,
//						body );
//
//					Owner.mailLogger.Log( "Sending notify e-mail:\n" + subject + "\n" + body );
//				}
//			}
			
		}

		public string ExpandVariables( string input )
		{
			string result = input;

			// --
			// Build the $(Ports) variable.

			string ports_text = "";

			foreach ( Port p in UnreachablePorts )
			{
				string port_text = 
					p.ExpandVariables( Owner.Settings.EMailPortNotAvailableBodyPort );
 
				/*
				if ( ports_text.Length>0 )
					ports_text += "\n";
				*/
				ports_text += port_text;
//				ports_text = "\n" + port_text;
			}

			// --

			result = result.Replace( "$(Ports)", ports_text );
			result = result.Replace( "$(HostName)", Address );
			string ip;
			if (IP != null)
			{
				ip = IP.ToString();
				result = result.Replace( "$(HostIP)", ip);
			}
			else
			{
				ip = IP.ToString();
				result = result.Replace( "$(HostIP)", "");
			}
			result = result.Replace( "$(Now)", DateTime.Now.ToString() );

			return result;
		}

		// --

		public IPAddress IP
		{ 
			get 
			{ 
				IPAddress ip;
				try
				{
					ip = Dns.Resolve(Address).AddressList[0];
					return ip;
				}
				catch (Exception)
				{
					return null;
				}
			}
		}

		public Configuration Owner;

		public bool IsActive;

		public string Address;
		public Port[] Ports;
		
		public ArrayList mailAdressList = new ArrayList();
		public Hashtable PortTable = new Hashtable();
		
		public string[] EMailPortNotAvailableReceivers;

		// --

		// A Port object inserts itselfe here.
		public ArrayList UnreachablePorts = new ArrayList();
		public ArrayList ReachablePorts   = new ArrayList();
	}

	/////////////////////////////////////////////////////////////////////////
	/// In-memory representation of ONE port to check for a certain server.

	public class Port
	{

		public Port()
		{
		}

		public Port( Server owner, XmlNode port_node )
		{
			Owner = owner;

			// --

			XmlAttribute a = port_node.Attributes["number"];
			if ( a!=null )
				Number = Convert.ToInt32(a.Value);
			else
				Number = (-1);

			a = port_node.Attributes["numberRangeBegin"];
			if ( a!=null )
				NumberRangeBegin = Convert.ToInt32(a.Value);
			else
				NumberRangeBegin = (-1);

			a = port_node.Attributes["numberRangeEnd"];
			if ( a!=null )
				NumberRangeEnd = Convert.ToInt32(a.Value);
			else
				NumberRangeEnd = (-1);

			// --

			a = port_node.Attributes["addressFamily"];
			if ( a!=null )
				AddressFamily = (AddressFamily)AddressFamily.Parse(typeof(AddressFamily),Convert.ToString(a.Value),true);
			else
				AddressFamily = AddressFamily.InterNetwork;

			a = port_node.Attributes["socketType"];
			if ( a!=null )
				SocketType = (SocketType)SocketType.Parse(typeof(SocketType),Convert.ToString(a.Value),true);
			else
				SocketType = SocketType.Stream;

			a = port_node.Attributes["protocolType"];
			if ( a!=null )
				ProtocolType = (ProtocolType)ProtocolType.Parse(typeof(ProtocolType),Convert.ToString(a.Value),true);
			else
				ProtocolType = ProtocolType.Tcp;
		}

		// --

		public void Process()
		{
			if ( HasNumber )
			{
				ProcessPort( Number );
			}

			if ( HasRange )
			{
				for ( int number=NumberRangeBegin; number<=NumberRangeEnd; ++number )
					ProcessPort( number );
			}
		}

		public void ProcessPort( int number )
		{
			Socket socket = new Socket( AddressFamily, SocketType, ProtocolType );
			IPEndPoint ep = new IPEndPoint( Owner.IP, number );

			try
			{
				socket.Connect( ep );
				if ( !socket.Connected )
				{
					Exception = new Exception("Socket.Connect() returned false.");
					ExceptionPortNumber = number;
				}
				else
				{
					socket.Close();
				}
			}
			catch ( SocketException e )
			{
				Exception = e;
				ExceptionPortNumber = number;

				socket.Close();
			}
		}

		public void OnCantConnect()
		{
			// Postphone to send later.
			Owner.UnreachablePorts.Add( this );
		}

		public string ExpandVariables( string input )
		{
			string result = input;

			result = result.Replace( "$(Port)", Convert.ToString(Number) );
			result = result.Replace( "$(PortAddressFamily)", AddressFamily.ToString() );
			result = result.Replace( "$(PortSocketType)", SocketType.ToString() );
			result = result.Replace( "$(PortProtocolType)", ProtocolType.ToString() );
			//result = result.Replace( "$(HostName)", Owner.Address );
			result = result.Replace( "$(Now)", DateTime.Now.ToString() );
			result = result.Replace( "$(ErrorMessage)", Exception!=null?Exception.Message:"(no exception)" );

			return result;
		}

		// --

		
		public Server Owner;
		
		public int Number;
		public int NumberRangeBegin;
		public int NumberRangeEnd;

		public bool HasNumber { get { return (Number != (-1)); } }
		public bool HasRange { get { return NumberRangeBegin!=(-1) && NumberRangeBegin<=NumberRangeEnd; } }

		public AddressFamily	AddressFamily;
		public SocketType		SocketType;
		public ProtocolType		ProtocolType;

		public int ExceptionPortNumber = 0;
		public Exception Exception = null;
	}


	public delegate void OnPortNotRespondeCallback(string server, int number, Exception e);
	public delegate void OnPortRespondeCallback(string server, int number);

	public class ServerPortPair
	{
		string _Server;
		int    _Number;

		public OnPortNotRespondeCallback onPortNotRespondeCallback;
		public OnPortRespondeCallback onPortRespondeCallback;

		public ServerPortPair(string server, int number,
			OnPortNotRespondeCallback NotResponde, OnPortRespondeCallback Responde)
		{
			_Server = server;
			_Number = number;
			onPortNotRespondeCallback = NotResponde;
			onPortRespondeCallback    = Responde;
		}

		public void Scan()
		{
			Socket socket = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
			IPAddress ip =null;
			try
			{
				ip = Dns.Resolve(_Server).AddressList[0];
			}
			catch(Exception e)
			{
				onPortNotRespondeCallback(_Server,_Number,e);
				//MessageBox.Show("Unable to resolve address ["+_Server+"]");
			}
			IPEndPoint ep = new IPEndPoint( ip, _Number );

			try
			{
				socket.Connect( ep );
				if ( !socket.Connected )
				{
					//onPortNotRespondeCallback(_Server,_Number,);
				}
				else
				{
					socket.Close();
					onPortRespondeCallback(_Server,_Number);

				}
			}
			catch ( SocketException e)
			{
				socket.Close();
				onPortNotRespondeCallback(_Server,_Number,e);
			}
		}
	}
	/////////////////////////////////////////////////////////////////////////
}