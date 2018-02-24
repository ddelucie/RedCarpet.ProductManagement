using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.ServiceProcess;
using System.Threading;
using NLog;
using RedCarpet.Data;
using System.IO;

namespace RedCarpet.MaxMin.FileConsumer
{
	class Program
	{

		static void Main(string[] args)
		{


			if (Environment.UserInteractive)
			{
				// running as console app
				MaxMinFileConsumer service = new MaxMinFileConsumer();
				service.Start(args);


				Console.WriteLine("Press any key to stop...");
				Console.ReadKey(true);

				service.Stop();
			}
			else
			{
				// running as service
				using (var service = new MaxMinFileConsumer())
				{
					ServiceBase.Run(service);
				}
			}


			Console.ReadKey();
		}




		public const string ServiceName = "MaxMin.FileConsumer";

		public class MaxMinFileConsumer : ServiceBase
		{

			ILogger nLogger = LogManager.GetLogger("MaxMin Consumer Logger");
			IDataRepository dataRepository = new DataRepository();
			string maxMinFilePath;
			Thread thread;
	 

			public MaxMinFileConsumer()
			{
				try
				{
					ServiceName = Program.ServiceName;


					nLogger.Log(LogLevel.Info, "AppSettings Initializing");

					var appSettings = ConfigurationManager.AppSettings;
					maxMinFilePath = appSettings["maxMinFilePath"];

					nLogger.Log(LogLevel.Info, "AppSettings Initialized");

					
				}
				catch (Exception ex)
				{
					nLogger.Log(LogLevel.Error, ex);
					Stop();
				}

			}

			protected override void OnStart(string[] args)
			{
				Console.WriteLine("Starting " + ServiceName);
				nLogger.Log(LogLevel.Info, "*** Starting " + ServiceName);
				try
				{
					FileSystemWatcher watcher = new FileSystemWatcher();
					watcher.Path = maxMinFilePath;
					watcher.Created += new FileSystemEventHandler(OnChanged);
					// Begin watching.
					watcher.EnableRaisingEvents = true;
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}

			private void OnChanged(object sender, FileSystemEventArgs e)
			{
				nLogger.Log(LogLevel.Info, "OnChanged event raised, file: {0}" + e.FullPath);

			}

			protected override void OnStop()
			{
				Console.WriteLine("Stopping " + ServiceName);
				nLogger.Log(LogLevel.Info, "*** Stopping " + ServiceName);
				thread.Abort();
			}
			public void DoWork()
			{
				while (true)
				{
					
				}
			}

			public void Start(string[] args)
			{
				this.OnStart(args);
			}
			public new void Stop()
			{
				this.OnStop();
			}
		}

	}
}
