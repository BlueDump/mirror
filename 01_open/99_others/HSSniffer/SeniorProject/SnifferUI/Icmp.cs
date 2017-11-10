using System;
using System.Net;
using System.Net.Sockets;

namespace SnifferUI
{
	/// <summary>
	/// Summary description for IcmpPacket.
	/// </summary>
	public class Icmp
	{
		private IPEndPoint Source_ = null;
		private IPEndPoint Destination_ = null;
		private byte Type_;
		private byte Code_;
        private UInt16 Checksum_;
		private int DataSize_;
		private byte[] Data_ = new byte[1024];

		public Icmp()
		{
			
		}

		public IPEndPoint Source 
		{ 
			get { return Source_; }
			set { Source_ = value; }
		}

		public IPEndPoint Destination 
		{ 
			get { return Destination_; }
			set { Destination_ = value; }
		}

		public String DestinationIP
		{ 
			get { return this.Destination_.Address.ToString(); }
		}

		public String SourceIP 
		{ 
			get { return this.Source_.Address.ToString(); }
		}

		public byte Type
		{ 
			get { return Type_; }
			set {Type_=value;}
		}

		public byte Code
		{ 
			get { return Code_; }
			set {Code_=value;}
		}

		public UInt16 Checksum
		{ 
			get { return Checksum_; }
			set {Checksum_=value;}
		}

		public int DataSize
		{ 
			get { return DataSize_; }
			set {DataSize_=value;}
		}

		public Byte[] Data
		{ 
			get { return Data_; }
			set {Data_=value;}
		}

		public void SetData(byte[] data, int offset, int length) 
		{
			Data_ = new byte[length];
			Array.Copy(data,offset,Data_,0,length);
		}

		public String GetHashString() 
		{ 
			String format = "Icmp:{0}:{1}";
			return String.Format(format,this.SourceIP,this.DestinationIP);
		}


		public Icmp(byte[] data, int size)
		{
			Type_ = data[20];
			Code_ = data[21];
			Checksum_ = BitConverter.ToUInt16(data, 22);
			DataSize_ = size - 24;
			Buffer.BlockCopy(data, 24, Data_, 0, DataSize_);
		}
		public byte[] getBytes()
		{
			byte[] data = new byte[DataSize_ + 9];
			Buffer.BlockCopy(BitConverter.GetBytes(Type_), 0, data, 0, 1);
			Buffer.BlockCopy(BitConverter.GetBytes(Code_), 0, data, 1, 1);
			Buffer.BlockCopy(BitConverter.GetBytes(Checksum_), 0, data, 2, 2);
			Buffer.BlockCopy(Data_, 0, data, 4, DataSize_);
			return data;
		}
		public UInt16 getChecksum()
		{
			UInt32 chcksm = 0;
			byte[] data = getBytes();
			int packetsize = DataSize_ + 8;
			int index = 0;
			while ( index < packetsize)
			{
				chcksm += Convert.ToUInt32(BitConverter.ToUInt16(data, index));
				index += 2;
			}
			chcksm = (chcksm >> 16) + (chcksm & 0xffff);
			chcksm += (chcksm >> 16);
			return (UInt16)(~chcksm);
		}
	}
}
