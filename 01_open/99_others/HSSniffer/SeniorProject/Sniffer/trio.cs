using System;

namespace Sniffer
{
	/// <summary>
	/// Summary description for trio.
	/// </summary>
	public class trio
	{
		private string source_;
		private string sPort_;
		private string dest_;
		private string dPort_;
		private string pro_;
		private string ProcStr_;
		private int Proc_;
		public trio()
		{
			source_="";
			sPort_="";
			dest_="";
			dPort_="";
			pro_="";
			ProcStr_="";

			//
			// TODO: Add constructor logic here
			//
		}
		public string Source 
		{ 
			get { return source_; }
			set { source_ = value; }
		}

		public string SourcePort 
		{ 
			get { return sPort_; }
			set { sPort_ = value; }
		}

		public string Destination
		{ 
			get { return dest_; }
			set { dest_ = value; }
		}
		public string DestinationPort 
		{ 
			get { return dPort_; }
			set { dPort_ = value; }
		}

		public string Protocol 
		{ 
			get { return pro_; }
			set { pro_ = value; }
		}

		public string ProcStr 
		{ 
			get { return ProcStr_; }
			set { ProcStr_ = value; }
		}
		public int Proc
		{ 
			get { return Proc_; }
			set { Proc_ = value; }
		}

		public bool checkAvailable(){
			if(Source!=""&&Destination!=""&&Protocol!=""&&SourcePort!="" &&DestinationPort!="")
				return true;
			return false;
		}

		public String GetHashString() 
		{ 
			String format = "IPv4:{0}:{1}:{2}:{3}:{4}";
			return String.Format(format,Protocol,Source,Destination,SourcePort,DestinationPort);
		}
	}
}
