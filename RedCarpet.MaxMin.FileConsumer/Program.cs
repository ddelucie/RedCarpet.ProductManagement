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
using RedCarpet.Data.Model;

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
				try
				{

					nLogger.Log(LogLevel.Info, "OnChanged event raised, file: {0}" + e.FullPath);


					var csvData = File.ReadAllLines(e.FullPath);

					nLogger.Log(LogLevel.Info, "ReadAllLines, count: {0}" + csvData.Count());


					var maxMinList = new List<Product>();

					maxMinList = csvData
									  .Skip(1)
									  .Select(line => FromCsv(line))
									  .ToList();

					nLogger.Log(LogLevel.Info, "Parsed, count: {0}" + maxMinList.Count());


					IList<Product> existingProducts = dataRepository.GetAll<Product>();
					IList<Product> savingProducts = new List<Product>();
					foreach (var existingProduct in existingProducts)
					{
						Product newValues = maxMinList.Where(p => p.ItemNumber == existingProduct.ItemNumber).FirstOrDefault();
						if (newValues != null)
						{
							existingProduct.MaxAmazonSellPrice = newValues.MaxAmazonSellPrice;
							existingProduct.MinAmazonSellPrice = newValues.MinAmazonSellPrice;
							existingProduct.DateUpdated = DateTime.UtcNow;
							savingProducts.Add(existingProduct);
						}
					}

					nLogger.Log(LogLevel.Info, "dataRepository.UpdateList, count: {0}" + savingProducts.Count());

					//B006FUXAGU
					dataRepository.UpdateList(savingProducts);

					nLogger.Log(LogLevel.Info, "Products saved");

				}
				catch (Exception ex)
				{
					nLogger.Log(LogLevel.Error, ex.Message);
				}
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


		public static Product FromCsv(string csvLine)
		{
			string[] values = csvLine.Split(',');
			Product product = new Product();
			product.ItemNumber = values[0];
			product.ASIN = values[1];
			product.MinAmazonSellPrice = Convert.ToDecimal(values[2].Replace("$", "").Replace("\"",""));
			product.MaxAmazonSellPrice = Convert.ToDecimal(values[3].Replace("$", "").Replace("\"", ""));

			return product;
		}


	}
}
