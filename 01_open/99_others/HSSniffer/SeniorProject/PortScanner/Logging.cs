namespace PortScannerNS
{
	using System;
	using System.Diagnostics;

	/////////////////////////////////////////////////////////////////////////

	/// <summary>
	/// Provides centralized logging access.
	/// </summary>

	public class Logging
	{
		public Logging(string pLogSource,string pLogName)
		{
			logSource = pLogSource;
			logName   = pLogName;
		}

		public string logSource ;
		public string logName   ;


		public void Log( string message )
		{
//			System.Console.Out.WriteLine( "[" + DateTime.Now.ToString() + "] " + message );
//			Debug.WriteLine( message );
			//LogEventLog( message );
		}

		public void LogError( string message )
		{
//			System.Console.Error.WriteLine( "[" + DateTime.Now.ToString() + "] " + message );
//			Debug.WriteLine( message );

			// Also to the event log.
			LogEventLogError( message );
		}

		public void LogEventLog( string message )
		{
			LogEventLog( message, EventLogEntryType.Information );
		}

		public void LogEventLogError( string message )
		{
			LogEventLog( message, EventLogEntryType.Error );
		}

		public void LogEventLog( string message, EventLogEntryType type )
		{	
			// Create the source, if it does not already exist.
			if ( !EventLog.SourceExists( logSource ) )
				EventLog.CreateEventSource( logSource, logName );
                
			// Create an EventLog instance and assign its source.
			EventLog log = new EventLog();
			log.Source = logSource;			
 
			// Write an informational entry to the event log.    
			log.WriteEntry( message, type );
		}
	}

	/////////////////////////////////////////////////////////////////////////
}