using System;
using Amazon.SimpleNotificationService.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RedCarpet.MWS.Common.Tests
{
	[TestClass]
	public class SubscriptionTests
	{
		[TestMethod]
		public void UpdateSub()
		{
			AWSCredentials awsCredentials = new AWSCredentials();
			SubscriptionWebService subscriptionWebService = new SubscriptionWebService(awsCredentials);
			var topics = subscriptionWebService.ListTopics();

			subscriptionWebService.DeleteTopic("Test321");
			subscriptionWebService.CreateTopic("Test321");

			topics = subscriptionWebService.ListTopics();

		}


	}
}
