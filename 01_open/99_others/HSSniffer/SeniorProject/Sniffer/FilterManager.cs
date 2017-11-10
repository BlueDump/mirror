using System;
using System.Collections;
using System.Net;

namespace Sniffer
{
	/// <summary>
	/// Summary description for FilterManager.
	/// </summary>
	
	public class FilterItem{
		public FilterItem(){
		}
		public bool mode = true; // is allow or is deny
		public string protocol ="";
		public string SourceAddress = "";
		public string DestinationAddress="";
		public int SourcePort=-1;
		public int DestinationPort=-1;
	}
	public class FilterManager
	{
		static ArrayList FilterList;
		static FilterManager()
		{
			FilterList= new ArrayList();
		}

		public static bool isAllowed(string pro,string SAd,string DAd,int SPort,int DPort){
			FilterItem FItem;
			for (int i=0;i<FilterList.Count;i++)
			{
				FItem = (FilterItem)FilterList[i];

				if (FItem.mode)
				{
//					if (FItem.protocol!=pro) return false;
//					if (FItem.SourceAddress!="" && FItem.SourceAddress!=SAd) return false;
//					if (FItem.DestinationAddress!="" && FItem.DestinationAddress!=DAd) return false;
//					if (FItem.SourcePort!=-1 && FItem.SourcePort!=SPort) return false;
//					if (FItem.DestinationPort!=-1 && FItem.DestinationPort!=DPort) return false;

					if ((FItem.protocol!=pro)
						||(FItem.SourceAddress!="" && FItem.SourceAddress!=SAd)
						||(FItem.DestinationAddress!="" && FItem.DestinationAddress!=DAd)
						||(FItem.SourcePort!=-1 && FItem.SourcePort!=SPort)
						||(FItem.DestinationPort!=-1 && FItem.DestinationPort!=DPort)) return false;
				}
				else{
					if ((FItem.protocol==pro)
						&&(FItem.SourceAddress=="" || FItem.SourceAddress==SAd)
						&&(FItem.DestinationAddress=="" || FItem.DestinationAddress==DAd)
						&&(FItem.SourcePort==-1 || FItem.SourcePort==SPort)
						&&(FItem.DestinationPort==-1 || FItem.DestinationPort==DPort)) return false;
					
				}
			}
			return true;
		}

		public static void addFilter(FilterItem FItem)
		{
			FilterList.Add(FItem);
		}

		public static void removeFilter(FilterItem FItem)
		{
			FilterList.Remove(FItem);
		}

		public static void removeAtFilter(int off)
		{
			FilterList.RemoveAt(off);
		}

		public static void removeAll()
		{
			FilterList.Clear();
		}

		public static void swap(int off1,int off2)
		{
			FilterItem FItem=(FilterItem)FilterList[off1];
			FilterList[off1]=FilterList[off2];
			FilterList[off2]=FItem;
		}

		public static int FilterCount(){
			return FilterList.Count;
		}

		public static string ToString(int off){
			string ret="";
			bool all=true;
			FilterItem FItem=(FilterItem)FilterList[off];
			if (FItem.mode)
			{
				ret+="Allow ";
			}
			else
			{
				ret+="Deny ";
			}
			if (FItem.protocol!="")
			{
				ret+="Protocol :"+FItem.protocol;
				all=false;
			}
			if (FItem.SourceAddress!=""){
				ret+="Source Address : "+FItem.SourceAddress+" ";
				all=false;
			}
			if (FItem.SourcePort!=-1)
			{
				ret+="Source Port : "+FItem.SourcePort+" ";
				all=false;
			}
			if (FItem.DestinationAddress!="")
			{
				ret+="Destination Address : "+FItem.DestinationAddress+" ";
				all=false;
			}
			if (FItem.DestinationPort!=-1)
			{
				ret+="Destination Port : "+FItem.DestinationPort+" ";
				all=false;
			}
			if (all) ret+="All";
			return ret;
		}
	}
}
