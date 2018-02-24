namespace RedCarpet.MWS.Common
{
	public class SellerInfo
	{
		private string appName = "CSharpSampleCode";
		private string appVersion = "1.0";
		private string sellerId = "ARA1ZW7ZHL5MQ";
		private string mwsAuthToken = "example";
		private string marketplaceId = "ATVPDKIKX0DER";
		private string serviceUrl;
		private string queueUrl;


		public string SellerId
		{
			get
			{
				return this.sellerId;
			}
			set
			{
				this.sellerId = value;
			}
		}

		public string MarketplaceId
		{
			get
			{
				return this.marketplaceId;
			}
			set
			{
				this.marketplaceId = value;
			}
		}

		public string SqsServiceUrl
		{
			get
			{
				return this.serviceUrl;
			}
			set
			{
				this.serviceUrl = value;
			}
		}

		public string AppVersion
		{
			get
			{
				return this.appVersion;
			}
			set
			{
				this.appVersion = value;
			}
		}

		public string MwsAuthToken
		{
			get
			{
				return this.mwsAuthToken;
			}
			set
			{
				this.mwsAuthToken = value;
			}
		}

		public string QueueUrl
		{
			get
			{
				return queueUrl;
			}

			set
			{
				queueUrl = value;
			}
		}

		public bool UpdatePrices { get; set; }
		public int BatchSize { get; set; }
		public int BatchWaitTimeSec { get; set; }
		public int FeedSize { get; set; }
		public int BetweenFeedWaitTimeSec { get; set; }

		public string MwsServiceUrl { get; set; }

	}
}
