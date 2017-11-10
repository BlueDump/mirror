using System;
using System.Xml;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using Utility;

namespace PortScannerNS
{
	/// <summary>
	/// Summary description for Setting.
	/// </summary>
	/// 

	public class InvalidFieldException : Exception {}

	public class Setting
	{		

		public static string FileName="";
		private static string SettingFile = "PortScannerSettings";

		private static System.DateTime fileLastModifiedTime;
		

		static Setting()
		{
			FileInfo fileInfo = new FileInfo(SettingFile+".xml");
			if (!fileInfo.Exists)
			{
				XmlDocument doc = new XmlDocument();
				doc.LoadXml("<?xml version=\"1.0\"?><Setting><FileName></FileName></Setting>");
				XmlTextWriter writer = new XmlTextWriter(Setting.SettingFile+".xml",null);
				writer.Formatting = Formatting.Indented;
				doc.Save(writer);			
				if (writer!=null)
					writer.Close();
			}
			ReadFromFile();
			if (FileName != "")
			{
				try 
				{
					fileLastModifiedTime=FileUtility.getFileLastModifiedTime(FileName);
				}
				catch (Exception)
				{
					int pos = Setting.FileName.LastIndexOf("\\");
					string str = Setting.FileName.Remove(0,pos+1);
					Setting.FileName = str;
					Setting.WriteToFile();
					Setting.ReadFromFile();
					fileLastModifiedTime =FileUtility.getFileLastModifiedTime(FileName);
				}
			}
		}
		
//		public static Hashtable ViewSettings;
	
		public bool isLoadedFileChanged
		{
			get
			{
				return ((FileUtility.getFileLastModifiedTime(FileName)
					!= fileLastModifiedTime)?true:false);
			}
		}
		
		public static void ReadFromFile()
		{
			string str = xmlReader(SettingFile);
			if (str==null)
			{
				MessageBox.Show("Parse error in file "+SettingFile+".xml","Setting File Error",
					MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
			XmlDocument doc1 = new XmlDocument();
			doc1.LoadXml(str);
			XmlNode root = doc1.DocumentElement;
			
			foreach (XmlNode item in root.ChildNodes)
			{
				switch (item.Name)
				{
					case"FileName":
						FileName=item.InnerText.ToString();
						break;
			
					default:
						MessageBox.Show("Invalid fields in "+SettingFile+".xml","Setting File Error",
							MessageBoxButtons.OK,MessageBoxIcon.Error);
						break;
				}
			}
		}

		public static void WriteToFile()
		{
			XmlDocument doc = new XmlDocument();
			doc.LoadXml("<?xml version=\"1.0\"?><Setting></Setting>");

			XmlNode root = doc.DocumentElement;

			XmlNode newElem = doc.CreateElement("FileName");
			newElem.InnerText = Setting.FileName;
			root.AppendChild(newElem);

			
			XmlTextWriter writer = new XmlTextWriter("PortScannerSettings.xml",null);
			writer.Formatting = Formatting.Indented;
			doc.Save(writer);			
			if (writer!=null)
				writer.Close();
			writer=null;
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
			catch(Exception )
			{	
				return null;
			}
		}

	}
}
