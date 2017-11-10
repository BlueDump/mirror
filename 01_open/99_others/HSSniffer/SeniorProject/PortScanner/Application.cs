/////////////////////////////////////////////////////////////////////////////
///
/// For questions or comments: mailto:uk@zeta-software.de
/// 
/// 2002-10-25
/// 
/////////////////////////////////////////////////////////////////////////////

namespace DfsmvPorty
{
	using System;
	using System.IO;


	/////////////////////////////////////////////////////////////////////////

	/// <summary>
	/// The main application class.
	/// </summary>

	class Application
	{
		[STAThread]
		static void Main( string[] args )
		{
			Configuration configuration = null;

			try
			{
				string job_path = DetectJobPath( args );

				// Loop.
				int looped_count = 0;
				while ( true )
				{
					looped_count++;

					// Re-read the configuration each pass.
					configuration = new Configuration( job_path );

					Logging.Log( "Configuation read successfully." );

					// --
					// Iterating all configurations.

					DateTime all_start = DateTime.Now;

					int index=0;
					foreach ( Server server in configuration.Servers )
					{
						index++;

						if ( !server.IsActive )
						{
							Logging.Log( "----------------" );
							Logging.Log( "Skipping inactive server (server " + index + " of " + configuration.Servers.Length + ")." );
							continue;
						}

						Logging.Log( "----------------" );
						Logging.Log( "Processing server " + index + " of " + configuration.Servers.Length + "..." );

						server.Process();
					}

					DateTime all_stop = DateTime.Now;
					TimeSpan all_span = all_stop-all_start;
					Logging.Log( "" );
					Logging.Log( "----------------" );
					Logging.Log( configuration.Servers.Length + " servers processed successfully." );

					// --

					// Check if need to exit looping.
					if ( !configuration.Settings.LoopEndless )
					{
						if ( looped_count>=configuration.Settings.LoopCount )
							break;
					}

					int interval = configuration.Settings.LoopIntervalSeconds;
					int sleep = interval - Convert.ToInt32(all_span.TotalSeconds);
					if ( sleep>0 )
					{
						Logging.Log( "Pausing " + (sleep*1000) + " seconds before next loop." );
						System.Threading.Thread.Sleep( sleep*1000 );
					}

					Logging.Log( "" );
					Logging.Log( "" );
					Logging.Log( "" );
					Logging.Log( "" );
					Logging.Log( "" );
					Logging.Log( "" );
					Logging.Log( "" );
					Logging.Log( "" );
					Logging.Log( "" );
				}
			}
			catch ( Exception e )
			{
				Logging.LogError( e.Message );
				Logging.LogError( e.Source );
				Logging.LogError( e.StackTrace );
			}

			if ( configuration!=null && configuration.Settings.WaitEndKey )
			{
				Logging.Log( "" );
				Logging.Log( "----------------" );
				Logging.Log( "Press <RETURN> to end!" );

				while ( System.Console.In.Peek()==-1 )
				{
					// Do nothing.
				}
			}
		}

		// --

		private const string JobFileExtension = ".dfsmvp";

		/// <summary>
		/// This function tries to get the complete path to the configuration
		/// file used to drive the application.
		/// </summary>
		/// <param name="args">The command line arguments of the application.</param>
		/// <returns>Throws an exception if cannot be detected.</returns>
		private static string DetectJobPath( string[] args )
		{
			if ( args.Length==0 )
				throw new Exception( 
					"No parameters where specified. Please specify the path to the *" + 
					JobFileExtension + " file." );

			string config_path = args[0];
			if ( !File.Exists( config_path ) )
			{
				if ( !File.Exists( config_path + JobFileExtension ) )
				{
					// Try path of the application (if present).
					string[] cla = System.Environment.GetCommandLineArgs();
					string app_path = cla[0];
					if ( app_path.IndexOf( '\\' )!=-1 )
					{
						string dir = app_path.Substring( 0, app_path.LastIndexOf( '\\' ) );
						config_path = dir + "\\" + config_path;
					}

					if ( !File.Exists( config_path ) )
					{
						if ( !File.Exists( config_path + JobFileExtension ) )
						{
							// Try in current directory.
							string dir = Directory.GetCurrentDirectory();
							dir.TrimEnd( '\\' );
							config_path = dir + "\\" + config_path;

							if ( !File.Exists( config_path ) )
							{
								if ( !File.Exists( config_path + JobFileExtension ) )
									throw new Exception( "The specified *" + JobFileExtension + " file does not exists." );
								else
									config_path += JobFileExtension;
							}
						}
						else
							config_path += JobFileExtension;
					}
				}
				else 
					config_path += JobFileExtension;
			}

			if ( !File.Exists( config_path ) )
				throw new Exception( "The specified configuration file '" + config_path + "' does not exists." );

			return config_path;
		}
	}

	/////////////////////////////////////////////////////////////////////////
}