using System;
using System.Net;
using IpHlpApidotnet;
using Sniffer;
using System.Diagnostics;

namespace SnifferUI
{
	/// <summary>
	/// Summary description for tables.
	/// </summary>
	public class tablesForProc
	{
		static IpHlpApidotnet.IPHelper myAPI;
		static tablesForProc()
		{
			
		}

		public static int getProcessId(IPv4Datagram datagram,string thisComputerIp)
		{
			myAPI= new IPHelper();
			string sourceIp="";
			string sourcePort="";
			string destIp="";
			string destPort="";
			switch (datagram.Protocol){
				case 6:
					TcpPacket tcpPacket =datagram.HandleTcpPacket(); 
					if(tcpPacket.SourceIP==thisComputerIp)
					{
						sourceIp=tcpPacket.SourceIP;
						sourcePort=tcpPacket.SourcePort.ToString();
						destIp=tcpPacket.DestinationIP;
						destPort=tcpPacket.DestinationPort.ToString();
					}
					else
					{
						sourceIp=tcpPacket.DestinationIP;
						sourcePort=tcpPacket.DestinationPort.ToString();
						destIp=tcpPacket.SourceIP;
						destPort=tcpPacket.SourcePort.ToString();
					}

					myAPI.GetExTcpConnexions();
					for(int i=0;i<myAPI.TcpExConnexions.dwNumEntries;i++)
					{
//						if (/*sourceIp==myAPI.TcpExConnexions.table[i].Local.Address.ToString()&&*/
//							sourcePort==myAPI.TcpExConnexions.table[i].Local.Port.ToString()//&&
//							/*destIp==myAPI.TcpExConnexions.table[i].Remote.Address.ToString()&&
//							destPort==myAPI.TcpExConnexions.table[i].Remote.Port.ToString()*/)
//						{
//							int ret = myAPI.TcpExConnexions.table[i].dwProcessId';
//							myAPI=null;
//							GC.Collect();
//							return ret;
//						}

						if (myAPI.TcpExConnexions.table[i].Remote.Port==0)
						{
							if (sourcePort==myAPI.TcpExConnexions.table[i].Local.Port.ToString())
							{
								int ret = myAPI.TcpExConnexions.table[i].dwProcessId;
								myAPI=null;
								GC.Collect();
								return ret;
							}
						}
						else{
							if (sourcePort==myAPI.TcpExConnexions.table[i].Local.Port.ToString()&&
								destPort==myAPI.TcpExConnexions.table[i].Remote.Port.ToString())
							{
								int ret = myAPI.TcpExConnexions.table[i].dwProcessId;
								myAPI=null;
								GC.Collect();
								return ret;
							}
						}
					}
					break;
				case 17:
					UdpDatagram udpPacket = datagram.HandleUdpDatagram();
					if(udpPacket.SourceIP==thisComputerIp)
					{
						sourceIp=udpPacket.SourceIP;
						sourcePort=udpPacket.SourcePort.ToString();
						destIp=udpPacket.DestinationIP;
						destPort=udpPacket.DestinationPort.ToString();
					}
					else
					{
						sourceIp=udpPacket.DestinationIP;
						sourcePort=udpPacket.DestinationPort.ToString();
						destIp=udpPacket.SourceIP;
						destPort=udpPacket.SourcePort.ToString();
					}
					myAPI.GetExUdpConnexions();
					for(int i=0;i<myAPI.UdpExConnexion.dwNumEntries;i++)
					{
						if (/*sourceIp==myAPI.UdpExConnexion.table[i].Local.Address.ToString()&&*/
							sourcePort==myAPI.UdpExConnexion.table[i].Local.Port.ToString())
						{						
							int ret= myAPI.UdpExConnexion.table[i].dwProcessId;
							myAPI=null;
							GC.Collect();
							return ret;
						}
					}
					break;
			}
			return -1;
		}
		public static string getProcessName(int processID)
		{
			//could be an error here if the process die before we can get his name
			try
			{
				Process p = Process.GetProcessById((int)processID);
				return p.ProcessName;
			}
			catch(Exception)
			{
				return "unknown";
			}
				
		}
	}
}
