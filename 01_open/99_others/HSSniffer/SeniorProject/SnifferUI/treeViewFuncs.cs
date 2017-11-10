using System;
using Sniffer;
using System.Windows.Forms;
using System.Net;
//using System.Diagnostics;

namespace SnifferUI
{
	/// <summary>
	/// Summary description for treeViewFuncs.
	/// </summary>
	public class treeViewFuncs
	{

		public treeViewFuncs()
		{

			//
			// TODO: Add constructor logic here
			//
		}

		public void addPacket(IPv4Datagram datagram,ref TreeView trv){
			int [] xx=new int[4];
			int total=datagram.Length+datagram.IHL;
			
			trv.Nodes.Clear();
			TreeNode tn = new TreeNode("Packet (Size : "+total.ToString()+" )");
			xx[0]=0;xx[1]=total*8-1; tn.Tag=xx;
			tn.ImageIndex=2;

			tn.Nodes.Add("IP");
			xx=new int[4];xx[0]=0;xx[1]=datagram.IHL*8-1;tn.Nodes[0].Tag=xx;
			tn.Nodes[0].ImageIndex=1;


			tn.Nodes[0].Nodes.Add("Version : 4");
			xx=new int[4];xx[0]=0;xx[1]=3;tn.Nodes[0].Nodes[0].Tag=xx;
			tn.Nodes[0].Nodes[0].ImageIndex=4;
			
			tn.Nodes[0].Nodes.Add("IHL : "+datagram.IHL);
			xx=new int[4];xx[0]=4;xx[1]=7;tn.Nodes[0].Nodes[1].Tag=xx;
			tn.Nodes[0].Nodes[1].ImageIndex=4;
			
			tn.Nodes[0].Nodes.Add("Service Type : "+consts.GetTOSForIP(datagram.TypeOfService));
			xx=new int[4];xx[0]=8;xx[1]=15;tn.Nodes[0].Nodes[2].Tag=xx;
			tn.Nodes[0].Nodes[2].ImageIndex=4;
			
			tn.Nodes[0].Nodes.Add("Total Length : "+ total.ToString());
			xx=new int[4];xx[0]=16;xx[1]=31;tn.Nodes[0].Nodes[3].Tag=xx;
			tn.Nodes[0].Nodes[3].ImageIndex=4;
			
			tn.Nodes[0].Nodes.Add("Identification : "+datagram.Identification.ToString());
			xx=new int[4];xx[0]=32;xx[1]=47;tn.Nodes[0].Nodes[4].Tag=xx;
			tn.Nodes[0].Nodes[4].ImageIndex=4;
			
			tn.Nodes[0].Nodes.Add("Ip Flags");
			xx=new int[4];xx[0]=48;xx[1]=50;tn.Nodes[0].Nodes[5].Tag=xx;
			tn.Nodes[0].Nodes[5].ImageIndex=4;
			
			tn.Nodes[0].Nodes[5].Nodes.Add("Reserved Flag : "+datagram.ReservedFlag.ToString());
			xx=new int[4];xx[0]=48;xx[1]=48;tn.Nodes[0].Nodes[5].Nodes[0].Tag=xx;
			tn.Nodes[0].Nodes[5].Nodes[0].ImageIndex=3;
			
			tn.Nodes[0].Nodes[5].Nodes.Add("Dont Fragment : "+datagram.DontFragmentFlag.ToString());
			xx=new int[4];xx[0]=49;xx[1]=49;tn.Nodes[0].Nodes[5].Nodes[1].Tag=xx;
			tn.Nodes[0].Nodes[5].Nodes[1].ImageIndex=3;
			
			tn.Nodes[0].Nodes[5].Nodes.Add("More Flag : "+datagram.MoreFlag.ToString());
			xx=new int[4];xx[0]=50;xx[1]=50;tn.Nodes[0].Nodes[5].Nodes[2].Tag=xx;
			tn.Nodes[0].Nodes[5].Nodes[2].ImageIndex=3;
			
			tn.Nodes[0].Nodes.Add("Fragment Offset : "+datagram.FragmentOffset.ToString());
			xx=new int[4];xx[0]=51;xx[1]=63;tn.Nodes[0].Nodes[6].Tag=xx;
			tn.Nodes[0].Nodes[6].ImageIndex=4;
			
			tn.Nodes[0].Nodes.Add("TTL : "+datagram.TTL.ToString());
			xx=new int[4];xx[0]=64;xx[1]=71;tn.Nodes[0].Nodes[7].Tag=xx;
			tn.Nodes[0].Nodes[7].ImageIndex=4;
			
			tn.Nodes[0].Nodes.Add("Upper Protcol : "+datagram.UpperProtocol);
			xx=new int[4];xx[0]=72;xx[1]=79;tn.Nodes[0].Nodes[8].Tag=xx;
			tn.Nodes[0].Nodes[8].ImageIndex=4;
			
			tn.Nodes[0].Nodes.Add("CheckSum : "+datagram.Checksum.ToString());
			xx=new int[4];xx[0]=80;xx[1]=95;tn.Nodes[0].Nodes[9].Tag=xx;
			tn.Nodes[0].Nodes[9].ImageIndex=4;
			
			tn.Nodes[0].Nodes.Add("Source : "+datagram.SourceName);
			xx=new int[4];xx[0]=96;xx[1]=127;tn.Nodes[0].Nodes[10].Tag=xx;
			tn.Nodes[0].Nodes[10].ImageIndex=4;
			
			tn.Nodes[0].Nodes.Add("Destination : "+datagram.DestinationName);
			xx=new int[4];xx[0]=128;xx[1]=159;tn.Nodes[0].Nodes[11].Tag=xx;
			tn.Nodes[0].Nodes[11].ImageIndex=4;
			
			if (datagram.IHL==24)
			{
				tn.Nodes[0].Nodes.Add("Options (+Padding): "+datagram.Options.ToString());
				xx=new int[4];xx[0]=160;xx[1]=datagram.IHL*8-1;tn.Nodes[0].Nodes[12].Tag=xx;	
				tn.Nodes[0].Nodes[12].ImageIndex=4;
			}
			
			
			if (datagram.UpperProtocol == "Tcp")
			{
				TcpPacket tcpPacket = datagram.HandleTcpPacket();
				tn.Nodes.Add("Tcp");
				xx=new int[4];xx[0]=datagram.IHL*8;xx[1]=total*8-1;tn.Nodes[1].Tag=xx;
				tn.Nodes[1].ImageIndex=1;
				
				tn.Nodes[1].Nodes.Add("Source Port : "+tcpPacket.SourcePort.ToString()+"  "+consts.GetTcpPorts(tcpPacket.SourcePort));
				xx=new int[4];xx[0]=datagram.IHL*8;xx[1]=datagram.IHL*8+15;tn.Nodes[1].Nodes[0].Tag=xx;
				tn.Nodes[1].Nodes[0].ImageIndex=4;
				
				tn.Nodes[1].Nodes.Add("Destination Port : "+tcpPacket.DestinationPort.ToString()+"  "+consts.GetTcpPorts(tcpPacket.DestinationPort));
				xx=new int[4];xx[0]=datagram.IHL*8+16;xx[1]=datagram.IHL*8+31;tn.Nodes[1].Nodes[1].Tag=xx;
				tn.Nodes[1].Nodes[1].ImageIndex=4;
				
				tn.Nodes[1].Nodes.Add("Sequence : "+tcpPacket.Sequence);
				xx=new int[4];xx[0]=datagram.IHL*8+32;xx[1]=datagram.IHL*8+63;tn.Nodes[1].Nodes[2].Tag=xx;
				tn.Nodes[1].Nodes[2].ImageIndex=4;
				
				tn.Nodes[1].Nodes.Add("Acknowledgement : "+tcpPacket.Acknowledgement);
				xx=new int[4];xx[0]=datagram.IHL*8+64;xx[1]=datagram.IHL*8+95;tn.Nodes[1].Nodes[3].Tag=xx;
				tn.Nodes[1].Nodes[3].ImageIndex=4;
				
				tn.Nodes[1].Nodes.Add("Header Length : "+tcpPacket.DataOffset);
				xx=new int[4];xx[0]=datagram.IHL*8+96;xx[1]=datagram.IHL*8+99;tn.Nodes[1].Nodes[4].Tag=xx;
				tn.Nodes[1].Nodes[4].ImageIndex=4;
				
				tn.Nodes[1].Nodes.Add("Flags");
				xx=new int[4];xx[0]=datagram.IHL*8+106;xx[1]=datagram.IHL*8+111;tn.Nodes[1].Nodes[5].Tag=xx;
				tn.Nodes[1].Nodes[5].ImageIndex=4;
				
				tn.Nodes[1].Nodes[5].Nodes.Add("x.....  URG : "+((tcpPacket.Urgent)? 1 : 0));
				xx=new int[4];xx[0]=datagram.IHL*8+106;xx[1]=datagram.IHL*8+106;tn.Nodes[1].Nodes[5].Nodes[0].Tag=xx;
				tn.Nodes[1].Nodes[5].Nodes[0].ImageIndex=3;
				
				tn.Nodes[1].Nodes[5].Nodes.Add(".x....  AKC : "+((tcpPacket.Ack)? 1 : 0));
				xx=new int[4];xx[0]=datagram.IHL*8+107;xx[1]=datagram.IHL*8+107;tn.Nodes[1].Nodes[5].Nodes[1].Tag=xx;
				tn.Nodes[1].Nodes[5].Nodes[1].ImageIndex=3;
				
				tn.Nodes[1].Nodes[5].Nodes.Add("..x...  EOL : "+((tcpPacket.Push)? 1 : 0));
				xx=new int[4];xx[0]=datagram.IHL*8+108;xx[1]=datagram.IHL*8+108;tn.Nodes[1].Nodes[5].Nodes[2].Tag=xx;
				tn.Nodes[1].Nodes[5].Nodes[2].ImageIndex=3;
				
				tn.Nodes[1].Nodes[5].Nodes.Add("...x..  RST : "+((tcpPacket.Reset)? 1 : 0));
				xx=new int[4];xx[0]=datagram.IHL*8+109;xx[1]=datagram.IHL*8+109;tn.Nodes[1].Nodes[5].Nodes[3].Tag=xx;
				tn.Nodes[1].Nodes[5].Nodes[3].ImageIndex=3;
				
				tn.Nodes[1].Nodes[5].Nodes.Add("....x.  SYN : "+((tcpPacket.Syn)? 1 : 0));
				xx=new int[4];xx[0]=datagram.IHL*8+110;xx[1]=datagram.IHL*8+110;tn.Nodes[1].Nodes[5].Nodes[4].Tag=xx;
				tn.Nodes[1].Nodes[5].Nodes[4].ImageIndex=3;
				
				tn.Nodes[1].Nodes[5].Nodes.Add(".....x  FIN : "+((tcpPacket.Fin)? 1 : 0));
				xx=new int[4];xx[0]=datagram.IHL*8+111;xx[1]=datagram.IHL*8+111;tn.Nodes[1].Nodes[5].Nodes[5].Tag=xx;
				tn.Nodes[1].Nodes[5].Nodes[5].ImageIndex=3;
				
				tn.Nodes[1].Nodes.Add("Window : "+tcpPacket.WindowSize);
				xx=new int[4];xx[0]=datagram.IHL*8+112;xx[1]=datagram.IHL*8+127;tn.Nodes[1].Nodes[6].Tag=xx;
				tn.Nodes[1].Nodes[6].ImageIndex=4;

				tn.Nodes[1].Nodes.Add("Checksum : "+tcpPacket.Checksum);
				xx=new int[4];xx[0]=datagram.IHL*8+128;xx[1]=datagram.IHL*8+143;tn.Nodes[1].Nodes[7].Tag=xx;
				tn.Nodes[1].Nodes[7].ImageIndex=4;

				tn.Nodes[1].Nodes.Add("Urgent Pointer : "+tcpPacket.UrgentPointer);
				xx=new int[4];xx[0]=datagram.IHL*8+144;xx[1]=datagram.IHL*8+159;tn.Nodes[1].Nodes[8].Tag=xx;
				tn.Nodes[1].Nodes[8].ImageIndex=4;

				if (tcpPacket.DataOffset>20)
				{
					tn.Nodes[1].Nodes.Add("Options + Padding : .....");
					xx=new int[4];xx[0]=datagram.IHL*8+160;xx[1]=datagram.IHL*8+tcpPacket.DataOffset*8-1;tn.Nodes[1].Nodes[9].Tag=xx;
					tn.Nodes[1].Nodes[9].ImageIndex=4;

					tn.Nodes[1].Nodes.Add("Data : "+".....");
					xx=new int[4];xx[0]=datagram.IHL*8+tcpPacket.DataOffset*8;xx[1]=total*8-1;tn.Nodes[1].Nodes[10].Tag=xx;
					tn.Nodes[1].Nodes[10].ImageIndex=4;
				}
				else{
					tn.Nodes[1].Nodes.Add("Data : "+".....");
					xx=new int[4];xx[0]=datagram.IHL*8+tcpPacket.DataOffset*8;xx[1]=total*8-1;tn.Nodes[1].Nodes[9].Tag=xx;
					tn.Nodes[1].Nodes[9].ImageIndex=4;
				}
				
			}
			else if	(datagram.UpperProtocol == "Udp")
			{
				UdpDatagram udpDatagram = datagram.HandleUdpDatagram();
				
				tn.Nodes.Add("Udp");
				xx=new int[4];xx[0]=datagram.IHL*8;xx[1]=total*8-1;tn.Nodes[1].Tag=xx;
				tn.Nodes[1].ImageIndex=1;
				
				tn.Nodes[1].Nodes.Add("Source Port : "+udpDatagram.SourcePort.ToString()+"  "+consts.GetTcpPorts(udpDatagram.SourcePort));
				xx=new int[4];xx[0]=datagram.IHL*8;xx[1]=datagram.IHL*8+15;tn.Nodes[1].Nodes[0].Tag=xx;
				tn.Nodes[1].Nodes[0].ImageIndex=4;
				
				tn.Nodes[1].Nodes.Add("Destination Port : "+udpDatagram.DestinationPort.ToString()+"  "+consts.GetTcpPorts(udpDatagram.DestinationPort));
				xx=new int[4];xx[0]=datagram.IHL*8+16;xx[1]=datagram.IHL*8+31;tn.Nodes[1].Nodes[1].Tag=xx;
				tn.Nodes[1].Nodes[1].ImageIndex=4;
				
				tn.Nodes[1].Nodes.Add("Other Fields + Data : "+".....");
				xx=new int[4];xx[0]=datagram.IHL*8+32;xx[1]=total*8-1;tn.Nodes[1].Nodes[2].Tag=xx;
				tn.Nodes[1].Nodes[2].ImageIndex=4;
				//					tn.Nodes[15].Nodes.Add("Length : "+udpDatagram.Length);
				//					tn.Nodes[15].Nodes.Add("Checksum : "+udpDatagram.Checksum);
					
			}
			else if (datagram.UpperProtocol == "ICMP")
			{
				IcmpPacket IPacket = datagram.HandleIcmpPacket();
				tn.Nodes.Add("Icmp");
				xx=new int[4];xx[0]=datagram.IHL*8;xx[1]=total*8-1;tn.Nodes[1].Tag=xx;
				tn.Nodes[1].ImageIndex=1;
				
				tn.Nodes[1].Nodes.Add("Type : "+consts.GetTypeForIcmp(IPacket.Type)+" {"+IPacket.Type.ToString()+"}");
				xx=new int[4];xx[0]=datagram.IHL*8;xx[1]=datagram.IHL*8+7;tn.Nodes[1].Nodes[0].Tag=xx;
				tn.Nodes[1].Nodes[0].ImageIndex=4;
				
				tn.Nodes[1].Nodes.Add("Code : "+consts.GetCodeForIcmp(IPacket.Type,IPacket.Code)+" {"+IPacket.Code.ToString()+"}");
				xx=new int[4];xx[0]=datagram.IHL*8+8;xx[1]=datagram.IHL*8+15;tn.Nodes[1].Nodes[1].Tag=xx;
				tn.Nodes[1].Nodes[1].ImageIndex=4;
				
				tn.Nodes[1].Nodes.Add("Checksum : "+IPacket.Checksum.ToString());
				xx=new int[4];xx[0]=datagram.IHL*8+16;xx[1]=datagram.IHL*8+31;tn.Nodes[1].Nodes[2].Tag=xx;
				tn.Nodes[1].Nodes[2].ImageIndex=4;
				
				if (IPacket.hasNextField)
				{
					int off=datagram.IHL*8+32;
					int i=0;
					for (i=0;i<IPacket.GetFieldCount();i++)
					{
						if (IPacket.GetFieldName(i)=="IP Address")
						{
							IPAddress ipa= new IPAddress(IPacket.GetField(i));
							tn.Nodes[1].Nodes.Add(IPacket.GetFieldName(i)+" : "+ipa.ToString());
						}
						else
							tn.Nodes[1].Nodes.Add(IPacket.GetFieldName(i)+" : "+IPacket.GetField(i).ToString());

						xx=new int[4];xx[0]=off;xx[1]=off+IPacket.GetFieldLengt(i)-1;tn.Nodes[1].Nodes[3+i].Tag=xx;
						tn.Nodes[1].Nodes[3+i].ImageIndex=4;
						off=off+IPacket.GetFieldLengt(i);
					}
					if (!IPacket.dataIsIp)
					{
						tn.Nodes[1].Nodes.Add("Data : "+".....");
						xx=new int[4];xx[0]=off;xx[1]=total*8-1;tn.Nodes[1].Nodes[3+i].Tag=xx;
						tn.Nodes[1].Nodes[3+i].ImageIndex=4;
					}
					else{
						tn.Nodes[1].Nodes.Add("Data : "+"(Has a Ip Packet)");
						xx=new int[4];xx[0]=off;xx[1]=total*8-1;tn.Nodes[1].Nodes[3+i].Tag=xx;
						tn.Nodes[1].Nodes[3+i].ImageIndex=1;
						
						tn.Nodes[1].Nodes[3+i].Nodes.Add("Identification : "+HeaderParser.ToInt(IPacket.Data,32,16).ToString());
						xx=new int[4];xx[0]=off+32;xx[1]=off+47;tn.Nodes[1].Nodes[3+i].Nodes[0].Tag=xx;
						tn.Nodes[1].Nodes[3+i].Nodes[0].ImageIndex=4;

						tn.Nodes[1].Nodes[3+i].Nodes.Add("Protocol : "+HeaderParser.ToInt(IPacket.Data,72,8).ToString());
						xx=new int[4];xx[0]=off+72;xx[1]=off+79;tn.Nodes[1].Nodes[3+i].Nodes[1].Tag=xx;
						tn.Nodes[1].Nodes[3+i].Nodes[1].ImageIndex=4;

						tn.Nodes[1].Nodes[3+i].Nodes.Add("Source IP : "+IPAddress.Parse(IPv4Datagram.GetIPString(HeaderParser.ToUInt(IPacket.Data,96,32))));
						xx=new int[4];xx[0]=off+96;xx[1]=off+127;tn.Nodes[1].Nodes[3+i].Nodes[2].Tag=xx;
						tn.Nodes[1].Nodes[3+i].Nodes[2].ImageIndex=4;

						tn.Nodes[1].Nodes[3+i].Nodes.Add("Destination IP : "+IPAddress.Parse(IPv4Datagram.GetIPString(HeaderParser.ToUInt(IPacket.Data,128,32))));
						xx=new int[4];xx[0]=off+128;xx[1]=off+159;tn.Nodes[1].Nodes[3+i].Nodes[3].Tag=xx;
						tn.Nodes[1].Nodes[3+i].Nodes[3].ImageIndex=4;

						tn.Nodes[1].Nodes[3+i].Nodes.Add("Data : .....");
						xx=new int[4];xx[0]=off+HeaderParser.ToInt(IPacket.Data,4,4)*32;xx[1]=total*8;tn.Nodes[1].Nodes[3+i].Nodes[4].Tag=xx;						
						tn.Nodes[1].Nodes[3+i].Nodes[4].ImageIndex=4;
					}
				}
				else{
					tn.Nodes[1].Nodes.Add("Other Fields + Data : "+".....");
					xx=new int[4];xx[0]=datagram.IHL*8+32;xx[1]=total*8-1;tn.Nodes[1].Nodes[3].Tag=xx;
					tn.Nodes[1].Nodes[3].ImageIndex=4;
				}
				
			}
			else{//Other Protocols
				ProtocolTemplate Others = datagram.HandleOthers();
				tn.Nodes.Add(Others.ProtocolName);
				xx=new int[4];xx[0]=datagram.IHL*8;xx[1]=total*8-1;tn.Nodes[1].Tag=xx;
				tn.Nodes[1].ImageIndex=1;
				int [] yy = new int[2]; 
				int off = datagram.IHL*8;
				int i=0;
				for(i=0;i<Others.GetFieldCount();i++)
				{
					tn.Nodes[1].Nodes.Add(Others.GetFieldName(i) + " : "+Others.GetField(i).ToString());
					yy=Others.GetFieldLength(i);
					xx=new int[4];xx[0]=off+yy[0];xx[1]=off+yy[1]+yy[0]-1;tn.Nodes[1].Nodes[i].Tag=xx;
					tn.Nodes[1].Nodes[i].ImageIndex=4;
				}

				tn.Nodes[1].Nodes.Add("Data : ..... ");				
				xx=new int[4];xx[0]=off+yy[1];xx[1]=total*8-1;tn.Nodes[1].Nodes[i].Tag=xx;
				tn.Nodes[1].Nodes[i].ImageIndex=4;
			}
			trv.Nodes.Add(tn);
			trv.ExpandAll();
			trv.Update();
		}

		public void HighlightText( RichTextBox RtxBin ,RichTextBox RtxHex, TreeNode MNode)
		{
			int StartX = 0, EndX = 0;
			int [] intArray;

			RtxBin.SelectionStart = 0;
			RtxBin.SelectionLength = RtxBin.Text.Length;
			RtxBin.SelectionColor = System.Drawing.Color.Black;

			RtxHex.SelectionStart = 0;
			RtxHex.SelectionLength = RtxHex.Text.Length;
			RtxHex.SelectionColor = System.Drawing.Color.Black;

			try
			{
				intArray = ( int [] ) MNode.Tag;
				if( intArray == null ) return;
			}
			catch
			{
				return;
			}

			StartX = intArray[0];
			EndX = intArray[1];

			RtxBin.SelectionStart = StartX+(StartX/32);
			RtxBin.SelectionLength = (EndX-StartX+1)+(EndX-StartX+1)/32;
			RtxBin.SelectionColor = System.Drawing.Color.Red;

			
			
			StartX = intArray[0]/4;
			EndX = intArray[1]/4;

			int s =intArray[0]/8;

			RtxHex.SelectionStart = StartX+s;
			RtxHex.SelectionLength = (EndX-StartX+1)+(EndX-StartX)/2;
			RtxHex.SelectionColor = System.Drawing.Color.Red;
		}
	}
}
