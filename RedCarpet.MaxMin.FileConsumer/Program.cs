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

namespace RedCarpet.MaxMin.FileConsumer
{
	class Program
	{

		static void Main(string[] args)
		{


			if (Environment.UserInteractive)
			{
				// running as console app
				SQSService service = new SQSService();
				service.Start(args);


				Console.WriteLine("Press any key to stop...");
				Console.ReadKey(true);

				service.Stop();
			}
			else
			{
				// running as service
				using (var service = new SQSService())
				{
					ServiceBase.Run(service);
				}
			}


			Console.ReadKey();
		}




		public const string ServiceName = "SQS Consumer";

		public class SQSService : ServiceBase
		{

			ILogger nLogger = LogManager.GetLogger("SQS Consumer Logger");
			IDataRepository dataRepository = new DataRepository();

			Thread thread;
	 

			public SQSService()
			{
				try
				{
					ServiceName = Program.ServiceName;


					nLogger.Log(LogLevel.Info, "AppSettings Initializing");

					var appSettings = ConfigurationManager.AppSettings;

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
				thread = new Thread(this.DoWork);
				thread.Start();
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
