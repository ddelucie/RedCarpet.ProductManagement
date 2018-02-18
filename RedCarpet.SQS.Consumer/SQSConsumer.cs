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

		public SQSConsumer()
		{ Initialize(); }

		public SQSConsumer(string queueUrl, string serviceUrl, ILogger nLogger, IDataRepository dataRepository)
		{
			nLogger.Log(LogLevel.Info, "SQSConsumer Initializing");
			nLogger.Log(LogLevel.Info, string.Format("serviceUrl: {0}", serviceUrl));
			nLogger.Log(LogLevel.Info, string.Format("queueUrl: {0}", queueUrl));

			this.queueUrl = queueUrl;
			this.serviceUrl = serviceUrl;
			this.nLogger = nLogger;
			this.dataRepository = dataRepository;
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
			receiveMessageRequest.MaxNumberOfMessages = 5;
			var receiveMessageResponse = sqsClient.ReceiveMessage(receiveMessageRequest);

			if (receiveMessageResponse.Messages.Count == 0) isQueueEmpty = true;

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

				ProcessMessage(notification);

				nLogger.Log(LogLevel.Info, string.Format("Processed notification"));

				DeleteMessge(message);

			}
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

		public void ProcessMessage(Notification notification)
		{
			string asin = notification.NotificationPayload.AnyOfferChangedNotification.OfferChangeTrigger.ASIN;

			Product product = dataRepository.GetFirstAsync<Product>(x => x.ASIN == asin).Result;

			PricingResult pricingResult = ProductLogic.SetPrice(notification, product);

			nLogger.Log(LogLevel.Info, string.Format("ASIN: {0}", pricingResult.ASIN));

			if (pricingResult.IsPriceChanged)
			{
				nLogger.Log(LogLevel.Info, string.Format("Price changed: {0}", pricingResult.NewPrice));

				// TODO: update price on Amazon
				//dataRepository.GetFirstAsync<>( )

				// update the product table
				product.CurrentPrice = pricingResult.NewPrice;
				dataRepository.Update(product);
			}

			//add history
			pricingResult.DateEntry = DateTime.UtcNow;
			dataRepository.Create(pricingResult);
		}
	}
}
