using System;
using System.Configuration;
using System.ServiceProcess;
using System.Threading;
using Amazon;
using Amazon.Runtime.CredentialManagement;
using NLog;
using RedCarpet.Data;
using RedCarpet.MWS.Common;

namespace RedCarpet.SQS.Consumer
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
			SellerInfo sellerInfo;

			public SQSService()
			{
				try
				{
					ServiceName = Program.ServiceName;


					nLogger.Log(LogLevel.Info, "AppSettings Initializing");

					var appSettings = ConfigurationManager.AppSettings;

					nLogger.Log(LogLevel.Info, "AppSettings Initialized");

					sellerInfo = new SellerInfo();
					sellerInfo.QueueUrl = appSettings["queueUrl"];
					sellerInfo.ServiceUrl = appSettings["sqsServiceUrl"];
					sellerInfo.MwsAuthToken = appSettings["mwsAuthToken"];
					sellerInfo.UpdatePrices = bool.Parse(appSettings["updatePrices"]);
					sellerInfo.BatchSize = int.Parse(appSettings["batchSize"]);
					sellerInfo.BatchWaitTimeSec = int.Parse(appSettings["batchWaitTimeSec"]);
					sellerInfo.FeedSize = int.Parse(appSettings["feedSize"]);
					sellerInfo.BetweenFeedWaitTimeSec = int.Parse(appSettings["betweenFeedWaitTimeSec"]);



					consumer = new SQSConsumer(sellerInfo, nLogger, dataRepository);
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
						nLogger.Log(LogLevel.Info, "Queue is empty, sleeping.");

						Thread.Sleep(5000);
					}
					else
					{
						/// wait before next batch
						nLogger.Log(LogLevel.Info, "Wait before next batch");

						Thread.Sleep(sellerInfo.BatchWaitTimeSec);
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
