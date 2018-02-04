using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace RedCarpet.SNS.Consumer
{
	public class SQSConsumer
	{
		string queueUrl = "https://sqs.us-west-2.amazonaws.com/324811268269/ConsoleTest";
		string serviceUrl = "http://sqs.us-west-2.amazonaws.com";

		public SQSConsumer()
		{
		}

		public SQSConsumer(string queueUrl, string serviceUrl)
		{
			this.queueUrl = queueUrl;
			this.serviceUrl = serviceUrl;
		}


		public void Process()
		{
			AmazonSQSConfig sqsConfig = new AmazonSQSConfig();

			sqsConfig.ServiceURL = serviceUrl;


			var sqsClient = new AmazonSQSClient(sqsConfig);

			

			var receiveMessageRequest = new ReceiveMessageRequest();

			receiveMessageRequest.QueueUrl = queueUrl;

			var receiveMessageResponse = sqsClient.ReceiveMessage(receiveMessageRequest);


			foreach (var item in receiveMessageResponse.Messages)
			{
				Console.WriteLine(item.Body);
				DeleteMessageResponse objDeleteMessageResponse = new DeleteMessageResponse();
				objDeleteMessageResponse = sqsClient.DeleteMessage(new DeleteMessageRequest()
				{ QueueUrl = queueUrl, ReceiptHandle = item.ReceiptHandle });
			}
		}
	}
}
