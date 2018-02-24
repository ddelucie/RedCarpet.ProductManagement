using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using MarketplaceWebService;
using MarketplaceWebService.Model;
using MarketplaceWebServiceProducts.Model;
using NLog;
using RedCarpet.MWS.Common;
using RedCarpet.MWS.Feeds.Feed;
using RedCarpet.MWS.Feeds.Model;

namespace RedCarpet.SQS.Consumer
{
	public class FeedHandler
	{
		SellerInfo sellerInfo;
		ILogger nLogger;
		MarketplaceWebServiceConfig config;
		MarketplaceWebService.MarketplaceWebService service;

		public FeedHandler(SellerInfo sellerInfo, ILogger nLogger)
		{
			this.sellerInfo = sellerInfo;
			this.nLogger = nLogger;

			var chain = new CredentialProfileStoreChain();
			AWSCredentials awsCredentials;
			chain.TryGetAWSCredentials("DD MWS", out awsCredentials);
			ImmutableCredentials creds = awsCredentials.GetCredentials();

			config = new MarketplaceWebServiceConfig();
			config.ServiceURL = sellerInfo.ServiceUrl;

			 service = new MarketplaceWebServiceClient(
					creds.AccessKey,
					creds.SecretKey,
					"x",
					sellerInfo.AppVersion,
					config);
		}

		public SubmitFeedResponse SubmitFeed(IList<RedCarpet.Data.Model.Product> products)
		{
			nLogger.Info("SubmitFeed");

			AmazonEnvelope amazonEnvelope = BuildAmazonEnvelope(products);
			SubmitFeedRequest submitFeedRequest = BuildSubmitFeedRequest(amazonEnvelope);
			SubmitFeedResponse submitFeedResponse = service.SubmitFeed(submitFeedRequest);
			return submitFeedResponse;
		}

		public AmazonEnvelope CheckFeedStatus(string feedSubmissionId)
		{
			GetFeedSubmissionResultRequest req = new GetFeedSubmissionResultRequest();
			req.MWSAuthToken = sellerInfo.MwsAuthToken;
			req.Merchant = sellerInfo.SellerId;
			req.FeedSubmissionId = feedSubmissionId;

			RedCarpet.MWS.Feeds.Model.AmazonEnvelope response = null;

			try
			{
				nLogger.Info("CheckFeedStatus");

				response = service.GetFeedSubmissionResultAmazonEnvelope(req);
			}
			catch (Exception e)
			{
				nLogger.Info("feed result not available");
			}

			return response;
		}

		public AmazonEnvelope  PollFeedStatus(string feedSubmissionId)
		{
			AmazonEnvelope  response = null;
			int sleepTime = 1000;
			for (int i = 0; i < 10; i++)
			{
				response = CheckFeedStatus(feedSubmissionId);
				if (response == null) sleepTime = 2 * sleepTime;
				Thread.Sleep(sleepTime * 1000);
			}

			return response;
		}


		public AmazonEnvelope BuildAmazonEnvelope(IList<RedCarpet.Data.Model.Product> products)
		{
			var feed = PriceFeedBuilder.Build();

			foreach (var product in products)
			{
				nLogger.Log(LogLevel.Info, string.Format("Adding to amazon feed. ASIN: {0}, SKU: {1}", product.ASIN, product.ItemNumber));

				RedCarpet.MWS.Feeds.Model.Message message = PriceFeedBuilder.BuildMessage();
				message.Price.StandardPrice.Value = product.CurrentPrice;
				message.Price.SKU = product.ItemNumber;
				message.MessageID = Guid.NewGuid().ToString();
				feed.Message.Add(message);

			}

			return feed;
		}

		public SubmitFeedRequest BuildSubmitFeedRequest(AmazonEnvelope amazonEnvelope)
		{
			SubmitFeedRequest submitFeedRequest = new SubmitFeedRequest();
			submitFeedRequest.MWSAuthToken = sellerInfo.MwsAuthToken;
			submitFeedRequest.Merchant = sellerInfo.SellerId;
			submitFeedRequest.FeedType = "_POST_PRODUCT_PRICING_DATA_";

			var stream = Util.GenerateStreamFromXml<AmazonEnvelope >(amazonEnvelope);
			submitFeedRequest.FeedContent = stream;
			submitFeedRequest.ContentMD5 = Util.CalculateContentMD5(stream);


			return submitFeedRequest;
		}

		 
	}
}
