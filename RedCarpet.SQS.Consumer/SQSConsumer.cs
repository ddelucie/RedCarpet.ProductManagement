using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Amazon.SQS;
using Amazon.SQS.Model;
using NLog;
using RedCarpet.Data;
using RedCarpet.Data.Model;
using RedCarpet.MWS.Common;

namespace RedCarpet.SQS.Consumer
{
	public class SQSConsumer
	{
		string queueUrl = "https://sqs.us-west-2.amazonaws.com/324811268269/ConsoleTest";
		string serviceUrl = "http://sqs.us-west-2.amazonaws.com";
		ILogger nLogger;
		IDataRepository dataRepository;
		AmazonSQSConfig sqsConfig;
		AmazonSQSClient sqsClient;
		SellerInfo sellerInfo;
		public SQSConsumer()
		{ Initialize(); }

		public SQSConsumer(SellerInfo sellerInfo, ILogger nLogger, IDataRepository dataRepository)
		{
			nLogger.Log(LogLevel.Info, "SQSConsumer Initializing");
			nLogger.Log(LogLevel.Info, string.Format("serviceUrl: {0}", serviceUrl));
			nLogger.Log(LogLevel.Info, string.Format("queueUrl: {0}", queueUrl));

			this.queueUrl = sellerInfo.QueueUrl;
			this.serviceUrl = sellerInfo.ServiceUrl;
			this.nLogger = nLogger;
			this.dataRepository = dataRepository;
			this.sellerInfo = sellerInfo;
			Initialize();
		}


		public void Initialize()
		{
			sqsConfig = new AmazonSQSConfig();

			sqsConfig.ServiceURL = serviceUrl;

			sqsClient = new AmazonSQSClient(sqsConfig);
		}

		public bool Process()
		{
			bool isQueueEmpty = false;
			var receiveMessageRequest = new ReceiveMessageRequest();

			receiveMessageRequest.QueueUrl = queueUrl;
			receiveMessageRequest.MaxNumberOfMessages = sellerInfo.BatchSize;
			var receiveMessageResponse = sqsClient.ReceiveMessage(receiveMessageRequest);
			nLogger.Log(LogLevel.Info, string.Format("Messages received, count: {0}", receiveMessageResponse.Messages.Count));


			if (receiveMessageResponse.Messages.Count == 0) isQueueEmpty = true;
			IList<PricingContext> pricingContexts = new List<PricingContext>();

			foreach (var message in receiveMessageResponse.Messages)
			{

				nLogger.Log(LogLevel.Info, string.Format("Message received: {0}", message.MessageId));

				Notification notification = null;

				try
				{
					notification = DeserializeNotification(message);
				}
				catch (Exception ex)
				{
					ex.Data.Add("message.Body", message.Body);
					nLogger.Log(LogLevel.Error, "DeserializeNotification failed");
					DeleteMessge(message);
					throw;
				}

				PricingContext pricingContext = ProcessMessage(notification);
				if (pricingContext != null) pricingContexts.Add(pricingContext);

				nLogger.Log(LogLevel.Info, string.Format("Processed notification"));

				DeleteMessge(message);

			}

			CommitResults(pricingContexts);
			var updatedProducts = UpdateAmazon(pricingContexts);
			CommitProducts(updatedProducts);
			

			return isQueueEmpty;
		}

		private Notification DeserializeNotification(Message message)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(Notification));

			Notification notification;

			using (TextReader reader = new StringReader(message.Body))
			{
				notification = (Notification)serializer.Deserialize(reader);
			}

			nLogger.Log(LogLevel.Info, string.Format("Deserialized notification"));
			return notification;
		}

		private void DeleteMessge(Message message)
		{
			DeleteMessageResponse objDeleteMessageResponse = new DeleteMessageResponse();
			var deleteMessageRequest = new DeleteMessageRequest() { QueueUrl = queueUrl, ReceiptHandle = message.ReceiptHandle };
			objDeleteMessageResponse = sqsClient.DeleteMessage(deleteMessageRequest);
			nLogger.Log(LogLevel.Info, string.Format("Message deleted: {0}", message.MessageId));
		}

		public PricingContext ProcessMessage(Notification notification)
		{
			string asin = notification.NotificationPayload.AnyOfferChangedNotification.OfferChangeTrigger.ASIN;

			Product product = dataRepository.GetFirstAsync<Product>(x => x.ASIN == asin).Result;
			if (product == null) return null;

			nLogger.Log(LogLevel.Info, string.Format("Product found for ASIN: {0}", product.ASIN));

			PricingResult pricingResult = ProductLogic.SetPrice(notification, product);

			nLogger.Log(LogLevel.Info, string.Format("SetPrice logic executed"));

			pricingResult.DateEntry = DateTime.UtcNow;

			if (pricingResult.IsPriceChanged)
			{
				nLogger.Log(LogLevel.Info, string.Format("New price selected: {0}", pricingResult.NewPrice));

				product.CurrentPrice = pricingResult.NewPrice;
				product.DateUpdated = DateTime.UtcNow;
			}

			return new PricingContext { Product = product, PricingResult = pricingResult };
		}

		private void CommitResults(IList<PricingContext> pricingContexts)
		{          
			nLogger.Log(LogLevel.Info, string.Format("Updating PricingResults in DB"));
			var pricingResults = pricingContexts.Select(pc => pc.PricingResult).ToList();
			dataRepository.CreateList(pricingResults);
		}

		private void CommitProducts(IList<Product> products)
		{
			nLogger.Log(LogLevel.Info, string.Format("Committing new prices to product table"));
			dataRepository.UpdateList(products);
		}

		private IList<Product> UpdateAmazon(IList<PricingContext> pricingContexts)
		{
			var products = pricingContexts.Where(pc => pc.PricingResult.IsPriceChanged).Select(pc => pc.Product).ToList();

			foreach (var product in products)
			{
				nLogger.Log(LogLevel.Info, string.Format("Adding to amazon feed. {0}", product.ASIN));
			}


			// TODO: update price on Amazon
			if (sellerInfo.UpdatePrices)
			{
				nLogger.Log(LogLevel.Info, string.Format("Updating Amazon"));
			}
			return products;
		}
	}
}
