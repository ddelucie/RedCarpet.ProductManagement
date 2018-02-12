using System;
using System.Configuration;
using System.ServiceProcess;
using System.Threading;
using NLog;
using RedCarpet.Data;

namespace RedCarpet.SNS.Consumer
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
			SQSConsumer consumer;

			public SQSService()
			{
				ServiceName = Program.ServiceName;

				var appSettings = ConfigurationManager.AppSettings;
				string queueUrl = appSettings["queueUrl"];
				string serviceUrl = appSettings["sqsServiceUrl"];

				consumer = new SQSConsumer(queueUrl, serviceUrl, nLogger, dataRepository);
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
					bool isQueueEmpty = false;
					try
					{
						isQueueEmpty = consumer.Process();
					}
					catch (Exception e)
					{
						nLogger.Log(LogLevel.Error, "*ERROR* " + e.Message);
					}
					if (isQueueEmpty)
					{
						nLogger.Log(LogLevel.Info, "Queue is empty");

						Thread.Sleep(10000);
					}
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
