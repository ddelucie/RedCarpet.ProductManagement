using System;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using MarketplaceWebService;
using MarketplaceWebService.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RedCarpet.MWS.Common.Tests
{
	[TestClass]
	public class FeedsTests
	{
		[TestMethod]
		public void GetReportCountTest()
		{
			var chain = new CredentialProfileStoreChain();
			AWSCredentials awsCredentials;
			chain.TryGetAWSCredentials("RedCarpet MWS", out awsCredentials);
			var creds = awsCredentials.GetCredentials();

			// The client application name
			string appName = "CSharpSampleCode";

			// The client application version
			string appVersion = "1.0";
			string serviceURL = "https://mws.amazonservices.com";

			string sellerId = "ARA1ZW7ZHL5MQ";
			string marketplaceId = "ATVPDKIKX0DER";
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
