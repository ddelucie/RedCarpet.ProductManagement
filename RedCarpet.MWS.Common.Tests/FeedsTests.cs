using System;
using System.IO;
using System.Xml.Serialization;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using MarketplaceWebService;
using MarketplaceWebService.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedCarpet.MWS.Feeds;
using RedCarpet.MWS.Feeds.Feed;

namespace RedCarpet.MWS.Common.Tests
{
	[TestClass]
	public class FeedsTests
	{

		ImmutableCredentials creds;
		// The client application name
		string appName = "CSharpSampleCode";

		// The client application version
		string appVersion = "1.0";
		string serviceURL = "https://mws.amazonservices.com";

		string sellerId = "A2FYRWBB6FC905";
		string marketplaceId = "ATVPDKIKX0DER";

		public FeedsTests()
		{
			var chain = new CredentialProfileStoreChain();
			AWSCredentials awsCredentials;
			chain.TryGetAWSCredentials("DD MWS", out awsCredentials);
			creds = awsCredentials.GetCredentials();
		}

		[TestMethod]
		public void SubmitFeedTest()
		{
			MarketplaceWebServiceConfig config = new MarketplaceWebServiceConfig();
			config.ServiceURL = serviceURL;

			MarketplaceWebService.MarketplaceWebService service =
				new MarketplaceWebServiceClient(
					creds.AccessKey,
					creds.SecretKey,
					appName,
					appVersion,
					config);

			SubmitFeedRequest submitFeedRequest = new SubmitFeedRequest();
			submitFeedRequest.MWSAuthToken = "amzn.mws.10b0d30f-3c9c-fa00-c792-e9142f66a94c";
			submitFeedRequest.Merchant = sellerId;
			submitFeedRequest.FeedType = "_POST_PRODUCT_PRICING_DATA_";
			PriceFeed priceFeed = PriceFeedBuilder.Build();
			priceFeed.Header.MerchantIdentifier = sellerId;
			Message message = PriceFeedBuilder.BuildMessage();
			message.MessageID = "1";
			message.Price.StandardPrice.Text = "66.00";
			message.Price.SKU = "8E-5FMM-A9HN";
			priceFeed.Messages.Add(message);
			var stream = Util.GenerateStreamFromXml<PriceFeed>(priceFeed);
			submitFeedRequest.FeedContent = stream;
			submitFeedRequest.ContentMD5 = Util.CalculateContentMD5(stream);
			SubmitFeedResponse submitFeedResponse = service.SubmitFeed(submitFeedRequest);

		}

		[TestMethod]
		public void GetFeedsTest()
		{
			MarketplaceWebServiceConfig config = new MarketplaceWebServiceConfig();
			config.ServiceURL = serviceURL;

			MarketplaceWebService.MarketplaceWebService service =
				new MarketplaceWebServiceClient(
					creds.AccessKey,
					creds.SecretKey,
					appName,
					appVersion,
					config);


			GetFeedSubmissionListRequest req = new GetFeedSubmissionListRequest();
			req.MWSAuthToken = "amzn.mws.10b0d30f-3c9c-fa00-c792-e9142f66a94c";
			req.Merchant = sellerId;
			var response = service.GetFeedSubmissionList(req);

		}


		[TestMethod]
		public void GetFeedTest()
		{
			MarketplaceWebServiceConfig config = new MarketplaceWebServiceConfig();
			config.ServiceURL = serviceURL;

			MarketplaceWebService.MarketplaceWebService service =
				new MarketplaceWebServiceClient(
					creds.AccessKey,
					creds.SecretKey,
					appName,
					appVersion,
					config);


			GetFeedSubmissionResultRequest req = new GetFeedSubmissionResultRequest();
			req.MWSAuthToken = "amzn.mws.10b0d30f-3c9c-fa00-c792-e9142f66a94c";
			req.Merchant = sellerId;
			req.FeedSubmissionId = "50002017580";
			var response = service.GetFeedSubmissionResult(req);

		}

		


		[TestMethod]
		public void GetReportCountTest()
		{
			
			MarketplaceWebServiceConfig config = new MarketplaceWebServiceConfig();
			config.ServiceURL = serviceURL;

			MarketplaceWebService.MarketplaceWebService service =
				new MarketplaceWebServiceClient(
					creds.AccessKey,
					creds.SecretKey,
					appName,
					appVersion,
					config);

			GetReportCountRequest request = new GetReportCountRequest();
			request.Merchant = sellerId;
			request.MWSAuthToken = ""; // Optional
			//@TODO: set additional request parameters here
			GetReportCountResponse response = service.GetReportCount(request);
		}
	}
	
}
