using System;

namespace Utility
{
	/// <summary>
	/// Summary description for Utility.
	/// </summary>
	/// 

	public class FileUtilityException : System.Exception
	{
	}

	public class FileUtility
	{
		static FileUtility()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static DateTime getFileLastModifiedTime(string fileName)
		{
			System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileName);

			if (!fileInfo.Exists)
				throw new FileUtilityException();
			
			return (fileInfo.LastWriteTime);
		}

		public static bool isFileExist(string fileName)
		{	
			System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileName);
			return (fileInfo.Exists);
		}
	}
}
