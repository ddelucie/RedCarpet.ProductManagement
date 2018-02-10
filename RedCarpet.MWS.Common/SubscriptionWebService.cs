using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

namespace RedCarpet.MWS.Common
{


	public class SubscriptionWebService
	{
		string serviceUrl = "http://sns.us-west-2.amazonaws.com";
		private AmazonSimpleNotificationServiceClient snsClient;
		private AWSCredentials aWSCredentials;
		public SubscriptionWebService(AWSCredentials aWSCredentials)
		{
			this.aWSCredentials = aWSCredentials;
			Initialize();
		}


		public void Initialize()
		{

			AmazonSimpleNotificationServiceConfig config = new AmazonSimpleNotificationServiceConfig();
			config.ServiceURL = serviceUrl;
			snsClient = new AmazonSimpleNotificationServiceClient(config);
		}


		public IList<Topic> ListTopics()
		{
			var request = new ListTopicsRequest();
			var response = new ListTopicsResponse();

			response = snsClient.ListTopics(request);
			return response.Topics;
		}
		public string CreateTopic(string name)
		{
			var request = new CreateTopicRequest();
			request.Name = name;
			var response = new CreateTopicResponse();

			response = snsClient.CreateTopic(request);
			return response.TopicArn;
		}

		public void Subscribe(string arn)
		{
			snsClient.Subscribe(new SubscribeRequest
			{ 
				Endpoint = "arn:aws:sqs:us-west-2:889329361753:AnyOfferChangedQueue",
				Protocol = "sqs",
				TopicArn = arn
			});
		}
		public void DeleteTopic(string arn)
		{
			var request = new DeleteTopicRequest();
			request.TopicArn = arn;
			var response = new DeleteTopicResponse();

			response = snsClient.DeleteTopic(request);
		}


		//public IMWSResponse Update(Subscription subscription)
		//{
		//	UpdateSubscriptionInput updateSubscriptionInput = new UpdateSubscriptionInput();
		//	updateSubscriptionInput.MarketplaceId = aWSCredentials.MarketplaceId;
		//	updateSubscriptionInput.MWSAuthToken = aWSCredentials.MwsAuthToken;
		//	updateSubscriptionInput.SellerId = aWSCredentials.SellerId;
		//	updateSubscriptionInput.Subscription = subscription;
		//	subscription.NotificationType = "AnyOfferChanged";
		//	return client.UpdateSubscription(updateSubscriptionInput);
		//}

		//public IMWSResponse RegisterDestination()
		//{
		//	// Create a request.
		//	RegisterDestinationInput registerDestinationInput = new RegisterDestinationInput();
		//	registerDestinationInput.MarketplaceId = aWSCredentials.MarketplaceId;
		//	registerDestinationInput.MWSAuthToken = aWSCredentials.MwsAuthToken;
		//	registerDestinationInput.SellerId = aWSCredentials.SellerId;
		//	Destination destination = new Destination();
		//	destination.DeliveryChannel = "SQS";
		//	AttributeKeyValue attributeKeyValue = new AttributeKeyValue() { Key = "sqsQueueUrl", Value = "" };
		//	AttributeKeyValueList attributeKeyValueList = new AttributeKeyValueList();
		//	attributeKeyValueList.Member.Add(attributeKeyValue);
		//	destination.AttributeList = attributeKeyValueList;
		//	registerDestinationInput.Destination = destination;

		//	return client.RegisterDestination(registerDestinationInput);
		//}

	}
}
