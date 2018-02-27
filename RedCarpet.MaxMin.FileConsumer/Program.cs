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
using System.Data.OleDb;
using System.Data;

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

					nLogger.Log(LogLevel.Info, "OnChanged event raised, file: " + e.FullPath);
					Thread.Sleep(5000);

					var fileName = e.FullPath;
					//var connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", fileName);
					string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=Excel 12.0;";

					var adapter = new OleDbDataAdapter("SELECT * FROM [MinMax$]", connectionString);
					var ds = new DataSet();

					adapter.Fill(ds, "MAXMIN");

					var maxMinList = ds.Tables["MAXMIN"].AsEnumerable();



					//var csvData = File.ReadAllLines(e.FullPath);

					//nLogger.Log(LogLevel.Info, "ReadAllLines, count: " + csvData.Count());

					//int skipCount = 0;
					//if (csvData[0].Contains("ItemNumber")) skipCount = 1;  

					//var maxMinList = new List<Product>();

					//maxMinList = csvData
					//				  .Skip(skipCount)
					//				  .Select(line => FromCsv(line))
					//				  .ToList();

					nLogger.Log(LogLevel.Info, "Parsed, count:" + maxMinList.Count());


					IList<Product> existingProducts = dataRepository.GetAll<Product>();
					IList<Product> savingProducts = new List<Product>();
					foreach (var existingProduct in existingProducts)
					{
						var newValues = maxMinList.Where(p => p.ItemArray[0].ToString() == existingProduct.ItemNumber).FirstOrDefault();
						if (newValues != null)
						{
							existingProduct.MaxAmazonSellPrice = decimal.Parse(newValues[3].ToString());
							existingProduct.MinAmazonSellPrice = decimal.Parse(newValues[2].ToString());
							existingProduct.DateUpdated = DateTime.UtcNow;
							savingProducts.Add(existingProduct);
						}
					}

					nLogger.Log(LogLevel.Info, "dataRepository.UpdateList, count: " + savingProducts.Count());

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
