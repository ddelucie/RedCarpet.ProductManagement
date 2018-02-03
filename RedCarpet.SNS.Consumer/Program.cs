using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
			AmazonSQSConfig sqsConfig = new AmazonSQSConfig();

			sqsConfig.ServiceURL = "http://sqs.us-west-2.amazonaws.com";


			var sqsClient = new AmazonSQSClient(sqsConfig);

			string queueUrl = "https://sqs.us-west-2.amazonaws.com/324811268269/ConsoleTest";

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
			Console.ReadKey();
		}
	}
}
