using System;

namespace Sniffer
{
	/// <summary>
	/// 
	/// </summary>
	public class SnifferException : System.Exception
	{
		public SnifferException(String msg) : this(msg,null)
		{
		}

		public SnifferException(String msg,Exception e ):base(msg,e) { 
		}
	}
}
