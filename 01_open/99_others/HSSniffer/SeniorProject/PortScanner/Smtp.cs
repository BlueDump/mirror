namespace PortScannerNS
{
	using System;

	/////////////////////////////////////////////////////////////////////////

	public class SmtpClient : System.Net.Sockets.TcpClient
	{
		/// <summary>
		/// Only use this, since it is creates exceptions.
		/// </summary>
		public static void Send( string server, string from, string to, string subject, string body )
		{
			SmtpMail mail = new SmtpMail();

			mail.MailFrom = from;
			mail.MailTo = to;
			mail.MailData = body;
			mail.Subject = subject;
			mail.SMTPServerIP = server;

			mail.SendMail();

			if ( mail.ErrorMessage.Length>0 )
				throw new ApplicationException( mail.ErrorMessage );
		}
		
		public bool IsConnected
		{
			get
			{
				return Active;
			}
		}
		public void SendCommandToServer(string Command)
		{
			System.Net.Sockets.NetworkStream ns = this.GetStream();
			byte[] WriteBuffer;
			WriteBuffer = new byte[1024];
			System.Text.ASCIIEncoding en = new System.Text.ASCIIEncoding();
			WriteBuffer = en.GetBytes(Command);
			ns.Write(WriteBuffer,0,WriteBuffer.Length);
			return;  
		}

		public string GetServerResponse()
		{
			int StreamSize;
			string ReturnValue = "";
			byte[] ReadBuffer;
			System.Net.Sockets.NetworkStream ns = this.GetStream();
			ReadBuffer = new byte[1024];
			StreamSize = ns.Read(ReadBuffer,0,ReadBuffer.Length);
			if (StreamSize==0)
			{
				return ReturnValue;
			}
			else
			{
				System.Text.ASCIIEncoding en = new System.Text.ASCIIEncoding();
				ReturnValue = en.GetString(ReadBuffer);
				return ReturnValue;
			}
		}

		public bool DoesStringContainSMTPCode(System.String s,System.String SMTPCode)
		{
			return (s.IndexOf(SMTPCode,0,10)==-1)?false:true;
		}
	}

	/////////////////////////////////////////////////////////////////

	public class SmtpMail
	{
		private System.String _SMTPServerIP = "127.0.0.1";
		private System.String _errmsg = "";
		private System.String _ServerResponse = "";
		private System.String _Identity = "expowin2k";
		private System.String _MailFrom = "";
		private System.String _MailTo = "";
		private System.String _MailData = "";
		private System.String _Subject = "";

		public System.String Subject // This property contains Subject of email
		{
			set
			{
				_Subject = value;
			}
		}

		public System.String Identity // This property contains Sender's Identity
		{
			set
			{
				_Identity = value;
			}
		}

		public System.String MailFrom // This property contains sender's email address
		{
			set
			{
				_MailFrom = value;
			}
		}

		public System.String MailTo // This property contains recepient's email address
		{
			set
			{
				_MailTo = value;
			}
		}

		public System.String MailData // This property contains email message
		{
			set
			{
				_MailData = value;
			}
		}

		public System.String SMTPServerIP // This property contains of SMTP server IP
		{
			get
			{
				return _SMTPServerIP;
			}
			set
			{
				_SMTPServerIP = value;
			}
		}

		public System.String ErrorMessage // This property contais error message
		{
			get
			{
				return _errmsg;
			}
		}

		public System.String ServerResponse // This property contains  server response
		{
			get
			{
				return _ServerResponse;
			}
		}

		public void SendMail()
		{
			try
			{
				System.String ServerResponse;
				SmtpClient tcp = new SmtpClient();  
				tcp.Connect(SMTPServerIP,25);// first connect to smtp server
				bool blnConnect = tcp.IsConnected;

				// test for successful connection
				if (!blnConnect)
				{
					_errmsg = "Connetion Failed...";
					return; 
				}

				//read response of the server  
				ServerResponse = tcp.GetServerResponse();
				if (tcp.DoesStringContainSMTPCode(ServerResponse,"220"))
				{
					_ServerResponse += ServerResponse;
				}
				else
				{
					_errmsg = "connection failed" + ServerResponse;  
					return;
				}
				System.String[] SendBuffer = new System.String[6];;
				System.String[] ResponseCode = {"220","250","251","354","221"}; 

				System.String StrTemp = "";
				StrTemp = System.String.Concat("HELO ",_Identity);
				StrTemp = System.String.Concat(StrTemp,"\r\n");
				SendBuffer[0] = StrTemp;
				StrTemp = "";
				StrTemp = System.String.Concat("MAIL FROM: ",_MailFrom);
				StrTemp = System.String.Concat(StrTemp,"\r\n");
				SendBuffer[1] = StrTemp;

				StrTemp = "";
				StrTemp = System.String.Concat("RCPT TO: ",_MailTo);
				StrTemp = System.String.Concat(StrTemp,"\r\n");
				SendBuffer[2] = StrTemp;

				StrTemp = "";
				StrTemp = System.String.Concat("DATA","\r\n");
				SendBuffer[3] = StrTemp;

				StrTemp = "";
				StrTemp = System.String.Concat("From: ",_MailFrom );
				StrTemp = System.String.Concat(StrTemp,"\r\n" );
				StrTemp = System.String.Concat(StrTemp,"To: " );
				StrTemp = System.String.Concat(StrTemp,_MailTo);
				StrTemp = System.String.Concat(StrTemp,"\r\n" );
				StrTemp = System.String.Concat(StrTemp,"Subject: " );
				StrTemp = System.String.Concat(StrTemp,_Subject);
				StrTemp = System.String.Concat(StrTemp,"\r\n" );

				StrTemp = System.String.Concat(StrTemp,_MailData);
				StrTemp = System.String.Concat(StrTemp,"\r\n.\r\n"); 
				SendBuffer[4] = StrTemp;

				StrTemp = "";
				StrTemp = System.String.Concat(StrTemp,"QUIT\r\n");
				SendBuffer[5] = StrTemp;

				int i = 0;

				while(i < SendBuffer.Length)
				{
					tcp.SendCommandToServer(SendBuffer[i]);  
					ServerResponse = tcp.GetServerResponse();
					for(int j=0;j<ResponseCode.Length;j++)
					{
						if (tcp.DoesStringContainSMTPCode(ServerResponse,ResponseCode[j]))
						{
							_ServerResponse += ServerResponse;
							_ServerResponse += "<br>";
							break;
						}
						else
						{
							if(j==ResponseCode.Length-1)
							{
								_errmsg += ServerResponse;
								_errmsg += SendBuffer[i];
								return;
							}
						}
					} 

					i++;
				}
			}
			catch(System.Net.Sockets.SocketException se)
			{
				_errmsg += se.Message + " " + se.StackTrace;
			}
			catch(System.Exception e)
			{
				_errmsg += e.Message + " " + e.StackTrace;
			}
		}
	}

	/////////////////////////////////////////////////////////////////////////
}