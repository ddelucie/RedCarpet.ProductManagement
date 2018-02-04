using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Amazon.SimpleNotificationService;
using Amazon.SQS;
using Amazon.SQS.Model;

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


			Thread thread;
			SQSConsumer consumer;

			public SQSService()
			{
				ServiceName = Program.ServiceName;

				var appSettings = ConfigurationManager.AppSettings;
				string queueUrl = appSettings["queueUrl"];
				string serviceUrl = appSettings["serviceUrl"];

				consumer = new SQSConsumer(queueUrl, serviceUrl);
			}

			protected override void OnStart(string[] args)
			{
				Console.WriteLine("Starting " + ServiceName);

				thread = new Thread(this.DoWork);
				thread.Start();
			}

			protected override void OnStop()
			{
				Console.WriteLine("Stopping " + ServiceName);

				thread.Abort();
			}
			public void DoWork()
			{
				while (true)
				{
					consumer.Process();
					Thread.Sleep(10000);
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
