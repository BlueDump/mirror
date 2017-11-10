using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace Sniffer
{
	public class DataManager
	{
		private Hashtable IPv4Table_ = null;

		public DataManager()
		{
			IPv4Table_ = new Hashtable();
		}

		public IPv4Datagram GetIPv4Datagram(int identification, IPAddress source, IPAddress dest) { 
			String format = "{0}:{1}:{2}";
			String ident = String.Format(format,identification,source.ToString(),dest.ToString());

			if ( IPv4Table_.Contains(ident) ) { 
				return (IPv4Datagram)IPv4Table_[ident];
			} else { 
				return null;
			}
		}

		public void AddIPv4Datagram(IPv4Datagram datagram) {
			Debug.Assert(!IPv4Table_.Contains(datagram.GetHashString()),"Datagram already in hashtable");
			IPv4Table_.Add(datagram.GetHashString(),datagram);
		}

		public void RemoveIPv4Datagram(IPv4Datagram datagram) { 
			Debug.Assert(IPv4Table_.Contains(datagram.GetHashString()),"Datagram not in the table");
			IPv4Table_.Remove(datagram.GetHashString());
		}
		public void Clear(){
			IPv4Table_.Clear();
		}
	}
}
