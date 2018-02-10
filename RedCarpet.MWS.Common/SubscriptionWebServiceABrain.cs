using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceWebServiceProducts;
//using FBAInboundServiceMWS.Model;
using MWSSubscriptionsService;
using MWSSubscriptionsService.Model;

namespace RedCarpet.MWS.Common
{


	public class SubscriptionWebServiceABrain
	{
		private MWSSubscriptionsServiceClient client;
		private AWSCredentials aWSCredentials;
		public SubscriptionWebServiceABrain(AWSCredentials aWSCredentials)
		{
			this.aWSCredentials = aWSCredentials;
			Initialize();
		}

		
		public void Initialize()
		{
			
			   MWSSubscriptionsServiceConfig config = new MWSSubscriptionsServiceConfig();
			config.ServiceURL = aWSCredentials.ServiceUrl;
			client = new MWSSubscriptionsServiceClient(aWSCredentials.AccessKey, aWSCredentials.AppVersion,
				"Red", aWSCredentials.SecretKey, config);
		}


		public Subscription GetSubscription()
		{
			GetSubscriptionInput getSubscriptionInput = new GetSubscriptionInput();
			getSubscriptionInput.MarketplaceId = aWSCredentials.MarketplaceId;
			getSubscriptionInput.MWSAuthToken = aWSCredentials.MwsAuthToken;
			getSubscriptionInput.SellerId = aWSCredentials.SellerId;
			Destination destination = new Destination();
			destination.DeliveryChannel = "SQS";
			AttributeKeyValue attributeKeyValue = new AttributeKeyValue() { Key = "sqsQueueUrl", Value = "https://sqs.us-west-2.amazonaws.com/889329361753/AnyOfferChangedQueue" };
			AttributeKeyValueList attributeKeyValueList = new AttributeKeyValueList();
			attributeKeyValueList.Member.Add(attributeKeyValue);
			destination.AttributeList = attributeKeyValueList;
			getSubscriptionInput.Destination = destination;
			return client.GetSubscription(getSubscriptionInput).GetSubscriptionResult.Subscription;
		}
		public IMWSResponse Update(Subscription subscription)
		{
			UpdateSubscriptionInput updateSubscriptionInput = new UpdateSubscriptionInput();
			updateSubscriptionInput.MarketplaceId = aWSCredentials.MarketplaceId;
			updateSubscriptionInput.MWSAuthToken = aWSCredentials.MwsAuthToken;
			updateSubscriptionInput.SellerId = aWSCredentials.SellerId;
			updateSubscriptionInput.Subscription = subscription;
			subscription.NotificationType = "AnyOfferChanged";
			return client.UpdateSubscription(updateSubscriptionInput);
		}

		public IMWSResponse RegisterDestination()
		{
			// Create a request.
			RegisterDestinationInput registerDestinationInput = new RegisterDestinationInput();
			registerDestinationInput.MarketplaceId = aWSCredentials.MarketplaceId;
			registerDestinationInput.MWSAuthToken = aWSCredentials.MwsAuthToken;
			registerDestinationInput.SellerId = aWSCredentials.SellerId;
			Destination destination = new Destination();
			destination.DeliveryChannel = "SQS";
			AttributeKeyValue attributeKeyValue = new AttributeKeyValue() { Key = "sqsQueueUrl", Value = "" };
			AttributeKeyValueList attributeKeyValueList = new AttributeKeyValueList();
			attributeKeyValueList.Member.Add(attributeKeyValue);
			destination.AttributeList = attributeKeyValueList;
			registerDestinationInput.Destination = destination;

			return client.RegisterDestination(registerDestinationInput);
		}
		
	}
}
