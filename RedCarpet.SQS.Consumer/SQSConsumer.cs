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
using MarketplaceWebService.Model;
using NLog;
using RedCarpet.Data;
using RedCarpet.Data.Model;
using RedCarpet.MWS.Common;
using RedCarpet.MWS.Feeds.Model;

namespace RedCarpet.SQS.Consumer
{
	public class SQSConsumer
	{
		string queueUrl;
		string serviceUrl = "http://sqs.us-west-2.amazonaws.com";
		ILogger nLogger;
		IDataRepository dataRepository;
		AmazonSQSConfig sqsConfig;
		AmazonSQSClient sqsClient;
		SellerInfo sellerInfo;
		FeedHandler feedHandler;

		public SQSConsumer()
		{ Initialize(); }

		public SQSConsumer(SellerInfo sellerInfo, ILogger nLogger, IDataRepository dataRepository)
		{
			nLogger.Log(LogLevel.Info, "SQSConsumer constructor");

			this.queueUrl = sellerInfo.QueueUrl;
			this.serviceUrl = sellerInfo.SqsServiceUrl;
			this.nLogger = nLogger;
			this.dataRepository = dataRepository;
			this.sellerInfo = sellerInfo;

			nLogger.Log(LogLevel.Info, string.Format("serviceUrl: {0}", serviceUrl));
			nLogger.Log(LogLevel.Info, string.Format("queueUrl: {0}", queueUrl));
			nLogger.Log(LogLevel.Info, string.Format("FeedSize: {0}", sellerInfo.FeedSize));
			nLogger.Log(LogLevel.Info, string.Format("BetweenFeedWaitTimeSec: {0}", sellerInfo.BetweenFeedWaitTimeSec));


			nLogger.Log(LogLevel.Info, "SQSConsumer Initializing");
			Initialize();

		}


		public void Initialize()
		{
			sqsConfig = new AmazonSQSConfig();

			sqsConfig.ServiceURL = serviceUrl;

			sqsClient = new AmazonSQSClient(sqsConfig);

			feedHandler = new FeedHandler(sellerInfo, nLogger);
		}

		public bool Process()
		{
			bool isQueueEmpty = false;

			List<Product> productsToUpdate = new List<Product>();
			int loops = 0;
			bool timeElapsed = false;
			bool feedSizeReached = false;
			DateTime startTime = DateTime.Now;

			while (true)
			{
				loops++;
				nLogger.Log(LogLevel.Info, string.Format("Loop {0}", loops));

				productsToUpdate.AddRange(ProcessMessages(out isQueueEmpty));

				if (startTime.AddSeconds(sellerInfo.BetweenFeedWaitTimeSec) < DateTime.Now) timeElapsed = true;
				if (productsToUpdate.Count > sellerInfo.FeedSize) feedSizeReached = true;


				if ((productsToUpdate.Count > 0) && 
					(isQueueEmpty ||
					feedSizeReached ||
					timeElapsed))
				{
					nLogger.Log(LogLevel.Info, string.Format("isQueueEmpty: {0}, feedSizeReached: {1}, timeElapse: {2}", isQueueEmpty, feedSizeReached, timeElapsed));

					var success = UpdateAmazon(productsToUpdate);
					if (success) CommitProducts(productsToUpdate);
					return isQueueEmpty;
				}

				if (isQueueEmpty) return isQueueEmpty;

			}
		}

		public IList<Product> ProcessMessages(out bool isQueueEmpty)
		{
			isQueueEmpty = false;
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

			CommitPricingContexts(pricingContexts);
			var productsToUpdate = GetProductsToUpdate(pricingContexts);


			return productsToUpdate;
		}

		private Notification DeserializeNotification(Amazon.SQS.Model.Message message)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(Notification));

			Notification notification;

			using (TextReader reader = new StringReader(message.Body))
			{
				notification = (Notification)serializer.Deserialize(reader);
			}

			return notification;
		}

		private void DeleteMessge(Amazon.SQS.Model.Message message)
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

		private void CommitPricingContexts(IList<PricingContext> pricingContexts)
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

		private IList<Product>  GetProductsToUpdate(IList<PricingContext> pricingContexts)
		{
			var products = pricingContexts.Where(pc => pc.PricingResult.IsPriceChanged).Select(pc => pc.Product).ToList();
			return products;
		}

		private bool UpdateAmazon(IList<Product> products)
		{
			bool success = false;
			// update price on Amazon
			if (sellerInfo.UpdatePrices)
			{
				nLogger.Log(LogLevel.Info, string.Format("Updating prices on Amazon"));
				SubmitFeedResponse submitFeedResponse = feedHandler.SubmitFeed(products);

				nLogger.Log(LogLevel.Info, string.Format("PollFeedStatus"));
				AmazonEnvelope result = feedHandler.PollFeedStatus(submitFeedResponse.SubmitFeedResult.FeedSubmissionInfo.FeedSubmissionId);

				nLogger.Log(LogLevel.Info, string.Format("Retrieved feed result"));

				if (result.Message.First().ProcessingReport.StatusCode == "Complete" &&
					result.Message.First().ProcessingReport.ProcessingSummary.MessagesSuccessful == "1")
				{
					nLogger.Log(LogLevel.Info, string.Format("Feed was a success"));
					success = true;
				}
			}
			else
			{
				nLogger.Log(LogLevel.Info, string.Format("Update pricse on Amazon Disabled"));
			}

			return success;
		}
	}
}
