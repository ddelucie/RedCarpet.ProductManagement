namespace RedCarpet.MWS.Common
{
	public class AWSCredentials
	{
		private string accessKey = "AKIAJZY7ZVPLTWQYHWYA";
		private string secretKey = " ";
		private string appName = "CSharpSampleCode";
		private string appVersion = "1.0";
		private string sellerId = "ARA1ZW7ZHL5MQ";
		private string mwsAuthToken = "example";
		private string marketplaceId = "ATVPDKIKX0DER";
		private string serviceUrl = "http://sns.us-west-2.amazonaws.com";

		public string AccessKey
		{
			get
			{
				return this.accessKey;
			}
			set
			{
				this.accessKey = value;
			}
		}

		public string SecretKey
		{
			get
			{
				return this.secretKey;
			}
			set
			{
				this.secretKey = value;
			}
		}

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

		public string ServiceUrl
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
	}
}
