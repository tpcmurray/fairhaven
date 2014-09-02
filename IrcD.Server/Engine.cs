using System;
using System.Configuration.Install;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;

namespace IrcD.Server
{
	class Engine
	{
		public static void Main(string[] args)
		{
			switch(Environment.OSVersion.Platform) {
			case PlatformID.MacOSX:
				Start();
				break;
			case PlatformID.Unix:
				Start();
				break;
			case PlatformID.Win32NT:
				if(Environment.UserInteractive) {
					var parameter = string.Concat(args);
					switch(parameter) {
					case "--install":
						ManagedInstallerClass.InstallHelper(new[] { Assembly.GetExecutingAssembly().Location });
						return;
					case "--uninstall":
						ManagedInstallerClass.InstallHelper(new[] { "/u", Assembly.GetExecutingAssembly().Location });
						return;
					}
					/* blocking */
					Start();
				}
				try {
					var servicesToRun = new ServiceBase[] { new ServiceEngine() };
					ServiceBase.Run(servicesToRun);
				} catch(Exception ex) {
					Console.WriteLine(string.Format("Exception: {0} \n\nStack: {1}", ex.Message, ex.StackTrace));
				}
				break;
			case PlatformID.Win32S:
				Console.WriteLine("16bit OS not supported...(STOP)");
				break;
			case PlatformID.Win32Windows:
				Start();
				break;
			case PlatformID.WinCE:
				Start();
				break;
			case PlatformID.Xbox:
				Start();
				break;
			default:
				Console.WriteLine("What kind of Platform are you?(STOP)");
				break;
			}
		}

		public static void Start()
		{
			var settings = new Settings();
			var ircDaemon = new IrcDaemon(settings.GetIrcMode());
			settings.setDaemon(ircDaemon);
			settings.LoadSettings();
			ircDaemon.ServerRehash += ServerRehash;
			var serverThread = new Thread(ircDaemon.Start);
			serverThread.IsBackground = false;
			serverThread.Name = "serverThread-1";
			serverThread.Start();
		}

		static void ServerRehash(object sender, RehashEventArgs e)
		{
			var settings = new Settings();
			settings.setDaemon(e.IrcDaemon);
			settings.LoadSettings();
		}
	}
}
