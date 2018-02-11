using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedCarpet.MWS.Common
{
	public class AWSCredentials
	{              
		// Developer AWS access key
		string accessKey = "AKIAJZY7ZVPLTWQYHWYA";

		// Developer AWS secret key
		string secretKey = "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";

		// The client application name
		string appName = "CSharpSampleCode";

		// The client application version
		string appVersion = "1.0";

		string sellerId = "ARA1ZW7ZHL5MQ";

		string mwsAuthToken = "example";

		string marketplaceId = "ATVPDKIKX0DER";

		string serviceUrl = "http://sns.us-west-2.amazonaws.com";

		public string AccessKey
		{
			get { return accessKey; }
			set { accessKey = value; }
		}

		public string SecretKey
		{
			get { return secretKey; }
			set { secretKey = value; }
		}

		public string SellerId
		{
			get { return sellerId; }
			set { sellerId = value; }
		}

		public string MarketplaceId
		{
			get { return marketplaceId; }
			set { marketplaceId = value; }
		}
		public string ServiceUrl
		{
			get { return serviceUrl; }
			set { serviceUrl = value; }
		}

		public string AppVersion
		{
			get { return appVersion; }
			set { appVersion = value; }
		}

		public string MwsAuthToken
		{
			get
			{
				return mwsAuthToken;
			}

			set
			{
				mwsAuthToken = value;
			}
		}
	}
}
