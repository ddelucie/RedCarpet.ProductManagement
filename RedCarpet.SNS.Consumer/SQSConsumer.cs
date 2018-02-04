using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using NLog;

namespace RedCarpet.SNS.Consumer
{
	public class SQSConsumer
	{
		string queueUrl = "https://sqs.us-west-2.amazonaws.com/324811268269/ConsoleTest";
		string serviceUrl = "http://sqs.us-west-2.amazonaws.com";
		ILogger nLogger;

		AmazonSQSConfig sqsConfig;

		public SQSConsumer()
		{ Initialize(); }

		public SQSConsumer(string queueUrl, string serviceUrl, ILogger nLogger)
		{
			nLogger.Log(LogLevel.Info, "SQSConsumer Initializing");

			this.queueUrl = queueUrl;
			this.serviceUrl = serviceUrl;
			this.nLogger = nLogger;
			Initialize();
		}


		public void Initialize()
		{
			sqsConfig = new AmazonSQSConfig();

			sqsConfig.ServiceURL = serviceUrl;

			nLogger.Log(LogLevel.Info, string.Format("serviceUrl: {0}", serviceUrl));
		}

		public void Process()
		{

			var sqsClient = new AmazonSQSClient(sqsConfig);

			var receiveMessageRequest = new ReceiveMessageRequest();

			receiveMessageRequest.QueueUrl = queueUrl;

			var receiveMessageResponse = sqsClient.ReceiveMessage(receiveMessageRequest);


			foreach (var message in receiveMessageResponse.Messages)
			{

				nLogger.Log(LogLevel.Info, string.Format("Message received: {0} || {1}", message.MessageId, message.Body));

				DeleteMessageResponse objDeleteMessageResponse = new DeleteMessageResponse();
				var deleteMessageRequest = new DeleteMessageRequest() { QueueUrl = queueUrl, ReceiptHandle = message.ReceiptHandle };
				objDeleteMessageResponse = sqsClient.DeleteMessage(deleteMessageRequest);

				nLogger.Log(LogLevel.Info, string.Format("Message deleted: {0}", message.MessageId));

			}
		}
	}
}
