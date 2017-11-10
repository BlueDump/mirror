using System;
using System.Xml;
using System.Collections;
using System.Windows.Forms;

namespace SnifferUI
{
	/// <summary>
	/// Summary description for Setting.
	/// </summary>
	/// 

	public class InvalidFieldException : Exception {}

	public class Setting
	{
		public static bool   SettingImportEnabled;
		public static string FileName;
		public static string PingType;
		public static string TraceRouteType;
		public static string PortSanProtocolType;

		static Setting()
		{
			ReadFromFile();
		}
		
		public static void ReadFromFile()
		{
			string str = xmlReader("Settings");
			XmlDocument doc1 = new XmlDocument();
			doc1.LoadXml(str);
			XmlNode root = doc1.DocumentElement;
			
			foreach (XmlNode item in root.ChildNodes)
			{
				switch (item.Name)
				{
					case"PingType":
						PingType=item.InnerText.ToString();
						break;
					case"TraceRouteType":
						TraceRouteType=item.InnerText.ToString();
						break;
					case"PortSanProtocolType":
						PortSanProtocolType=item.InnerText.ToString();
						break;
					default:
						throw (new InvalidFieldException());						
				}
			}
			
		}

		public static void WriteToFile()
		{
			XmlDocument doc = new XmlDocument();
			doc.LoadXml("<?xml version=\"1.0\"?><Setting></Setting>");

			XmlNode root = doc.DocumentElement;
//			root=root.LastChild;

			XmlElement newElem = doc.CreateElement("PingType");
			newElem.InnerText = Setting.PingType;
			root.AppendChild(newElem);

			newElem = doc.CreateElement("TraceRouteType");
			newElem.InnerText = Setting.TraceRouteType;
			root.AppendChild(newElem);

			newElem = doc.CreateElement("PortSanProtocolType");
			newElem.InnerText = Setting.PortSanProtocolType;
			root.AppendChild(newElem);

			XmlTextWriter writer = new XmlTextWriter("Settings.xml",null);
			writer.Formatting = Formatting.Indented;
			doc.Save(writer);			
			if (writer!=null)
				writer.Close();
		}

		public static string xmlReader(string filename)
		{
			string ret ="";
			XmlTextReader reader = null;

			try 
			{
				// Load the reader with the data file and ignore all white space nodes.         
				reader = new XmlTextReader(filename+".xml");
				reader.WhitespaceHandling = WhitespaceHandling.None;

				// Parse the file and display each of the nodes.
				while (reader.Read()) 
				{
					switch (reader.NodeType) 
					{
						case XmlNodeType.Element:
							ret+=String.Format("<{0}>", reader.Name);
							break;
						case XmlNodeType.Text:
							ret+=String.Format(reader.Value);
							break;
						case XmlNodeType.CDATA:
							ret+=String.Format("<![CDATA[{0}]]>", reader.Value);
							break;
						case XmlNodeType.ProcessingInstruction:
							ret+=String.Format("<?{0} {1}?>", reader.Name, reader.Value);
							break;
						case XmlNodeType.Comment:
							ret+=String.Format("<!--{0}-->", reader.Value);
							break;
						case XmlNodeType.XmlDeclaration:
							ret+=String.Format("<?xml version='1.0'?>");
							break;
						case XmlNodeType.Document:
							break;
						case XmlNodeType.DocumentType:
							ret+=String.Format("<!DOCTYPE {0} [{1}]", reader.Name, reader.Value);
							break;
						case XmlNodeType.EntityReference:
							ret+=String.Format(reader.Name);
							break;
						case XmlNodeType.EndElement:
							ret+=String.Format("</{0}>", reader.Name);
							break;
					}       
				}
				reader.Close();
				return ret;
			}
			catch(Exception)
			{
				if (filename=="Settings")
				{
					if (reader!=null)
						reader.Close();
					XmlDocument doc = new XmlDocument();
					doc.LoadXml("<?xml version=\"1.0\"?><Setting><PingType>Echo Request</PingType><TraceRouteType>Echo Request</TraceRouteType><PortSanProtocolType>Tcp</PortSanProtocolType></Setting>");
					XmlTextWriter writer = new XmlTextWriter("Settings.xml",null);
					writer.Formatting = Formatting.Indented;
					doc.Save(writer);			
					if (writer!=null)
						writer.Close();
					ret= "<?xml version=\"1.0\"?><Setting><PingType>Echo Request</PingType><TraceRouteType>Echo Request</TraceRouteType><PortSanProtocolType>Tcp</PortSanProtocolType></Setting>";
					return ret;
				}
			}
			return null;
		}

	}
}
