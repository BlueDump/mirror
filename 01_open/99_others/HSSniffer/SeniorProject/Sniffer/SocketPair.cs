using System;
using System.Net.Sockets;

namespace Sniffer
{
	/// <summary>
	/// Summary description for SocketPair.
	/// </summary>
	public class SocketPair
	{
		private Socket socket_ = null;
		private byte[] buffer_ = null;

		public Socket IPSocket { 
			get { return socket_; }
			set { socket_ = value; }
		}

		public byte[] Buffer { 
			get { return buffer_; }
			set { buffer_ = value; }
		}

		public SocketPair(Socket socket, byte[] buffer ) { 
			this.IPSocket = socket;
			this.Buffer = buffer;
		}
	}
}
