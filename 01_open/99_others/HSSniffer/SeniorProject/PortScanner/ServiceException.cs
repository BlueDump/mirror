using System;

namespace PortScannerNS
{
	/// <summary>
	/// Summary description for ServiceException.
	/// </summary>
	public class ServiceException : System.Exception
	{
		private string _Message;

		public string Message 
		{
			get 
			{
				return _Message;
			}
		}

		public ServiceException()
		{
			_Message = "Service Name not specified ...";
		}
	}
}
