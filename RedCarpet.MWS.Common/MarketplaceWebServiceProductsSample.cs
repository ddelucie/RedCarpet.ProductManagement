using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using FBAInboundServiceMWS.Model;
using MarketplaceWebService;
using MarketplaceWebServiceProducts;
using MarketplaceWebServiceProducts.Model;

namespace RedCarpet.MWS.Common
{
	namespace MarketplaceWebServiceProducts
	{

		/// <summary>
		/// Runnable sample code to demonstrate usage of the C# client.
		///
		/// To use, import the client source as a console application,
		/// and mark this class as the startup object. Then, replace
		/// parameters below with sensible values and run.
		/// </summary>
		public class MarketplaceWebServiceProductsSample
		{
			private MarketplaceWebServiceProductsClient client;

			public MarketplaceWebServiceProductsSample()
			{

			}
			public MarketplaceWebServiceProductsSample(MarketplaceWebServiceProductsClient client)
			{
				this.client = client;
			}


			public void RunTest()
			{
				// TODO: Set the below configuration variables before attempting to run

				// Developer AWS access key
				string accessKey = "AKIAJZY7ZVPLTWQYHWYA";

				// Developer AWS secret key
				string secretKey = "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";

				// The client application name
				string appName = "CSharpSampleCode";

				// The client application version
				string appVersion = "1.0";

				// The endpoint for region service and version (see developer guide)
				// ex: https://mws.amazonservices.com
				string serviceURL = "https://mws.amazonservices.com";

				// Create a configuration object
				MarketplaceWebServiceProductsConfig config = new MarketplaceWebServiceProductsConfig();
				config.ServiceURL = serviceURL;
				// Set other client connection configurations here if needed
				// Create the client itself
				MarketplaceWebServiceProductsClient client = new MarketplaceWebServiceProductsClient(appName, appVersion, accessKey, secretKey, config);

				this.client = client;

				// Uncomment the operation you'd like to test here
				// TODO: Modify the request created in the Invoke method to be valid

				try
				{
					IMWSResponse response = null;
					// response = sample.InvokeGetCompetitivePricingForASIN();
					// response = sample.InvokeGetCompetitivePricingForSKU();
					// response = sample.InvokeGetLowestOfferListingsForASIN();
					// response = sample.InvokeGetLowestOfferListingsForSKU();
					// response = sample.InvokeGetLowestPricedOffersForASIN();
					// response = sample.InvokeGetLowestPricedOffersForSKU();
					// response = sample.InvokeGetMatchingProduct();
					// response = sample.InvokeGetMatchingProductForId();
					// response = sample.InvokeGetMyFeesEstimate();
					 response = InvokeGetMyPriceForASIN();
					// response = sample.InvokeGetMyPriceForSKU();
					// response = sample.InvokeGetProductCategoriesForASIN();
					// response = sample.InvokeGetProductCategoriesForSKU();
					// response = sample.InvokeGetServiceStatus();
					// response = sample.InvokeListMatchingProducts();
					Console.WriteLine("Response:");
					ResponseHeaderMetadata rhmd = response.ResponseHeaderMetadata;
					// We recommend logging the request id and timestamp of every call.
					Console.WriteLine("RequestId: " + rhmd.RequestId);
					Console.WriteLine("Timestamp: " + rhmd.Timestamp);
					string responseXml = response.ToXML();
					Console.WriteLine(responseXml);
				}
				catch (MarketplaceWebServiceProductsException ex)
				{
					// Exception properties are important for diagnostics.
					ResponseHeaderMetadata rhmd = ex.ResponseHeaderMetadata;
					Console.WriteLine("Service Exception:");
					if (rhmd != null)
					{
						Console.WriteLine("RequestId: " + rhmd.RequestId);
						Console.WriteLine("Timestamp: " + rhmd.Timestamp);
					}
					Console.WriteLine("Message: " + ex.Message);
					Console.WriteLine("StatusCode: " + ex.StatusCode);
					Console.WriteLine("ErrorCode: " + ex.ErrorCode);
					Console.WriteLine("ErrorType: " + ex.ErrorType);
					throw ex;
				}
			}



			public GetCompetitivePricingForASINResponse InvokeGetCompetitivePricingForASIN()
			{
				// Create a request.
				GetCompetitivePricingForASINRequest request = new GetCompetitivePricingForASINRequest();
				string sellerId = "example";
				request.SellerId = sellerId;
				string mwsAuthToken = "example";
				request.MWSAuthToken = mwsAuthToken;
				string marketplaceId = "example";
				request.MarketplaceId = marketplaceId;
				ASINListType asinList = new ASINListType();
				request.ASINList = asinList;
				return this.client.GetCompetitivePricingForASIN(request);
			}

			public GetCompetitivePricingForSKUResponse InvokeGetCompetitivePricingForSKU()
			{
				// Create a request.
				GetCompetitivePricingForSKURequest request = new GetCompetitivePricingForSKURequest();
				string sellerId = "example";
				request.SellerId = sellerId;
				string mwsAuthToken = "example";
				request.MWSAuthToken = mwsAuthToken;
				string marketplaceId = "example";
				request.MarketplaceId = marketplaceId;
				SellerSKUListType sellerSKUList = new SellerSKUListType();
				request.SellerSKUList = sellerSKUList;
				return this.client.GetCompetitivePricingForSKU(request);
			}

			public GetLowestOfferListingsForASINResponse InvokeGetLowestOfferListingsForASIN()
			{
				// Create a request.
				GetLowestOfferListingsForASINRequest request = new GetLowestOfferListingsForASINRequest();
				string sellerId = "example";
				request.SellerId = sellerId;
				string mwsAuthToken = "example";
				request.MWSAuthToken = mwsAuthToken;
				string marketplaceId = "example";
				request.MarketplaceId = marketplaceId;
				ASINListType asinList = new ASINListType();
				request.ASINList = asinList;
				string itemCondition = "example";
				request.ItemCondition = itemCondition;
				bool excludeMe = true;
				request.ExcludeMe = excludeMe;
				return this.client.GetLowestOfferListingsForASIN(request);
			}

			public GetLowestOfferListingsForSKUResponse InvokeGetLowestOfferListingsForSKU()
			{
				// Create a request.
				GetLowestOfferListingsForSKURequest request = new GetLowestOfferListingsForSKURequest();
				string sellerId = "example";
				request.SellerId = sellerId;
				string mwsAuthToken = "example";
				request.MWSAuthToken = mwsAuthToken;
				string marketplaceId = "example";
				request.MarketplaceId = marketplaceId;
				SellerSKUListType sellerSKUList = new SellerSKUListType();
				request.SellerSKUList = sellerSKUList;
				string itemCondition = "example";
				request.ItemCondition = itemCondition;
				bool excludeMe = true;
				request.ExcludeMe = excludeMe;
				return this.client.GetLowestOfferListingsForSKU(request);
			}

			public GetLowestPricedOffersForASINResponse InvokeGetLowestPricedOffersForASIN()
			{
				// Create a request.
				GetLowestPricedOffersForASINRequest request = new GetLowestPricedOffersForASINRequest();
				string sellerId = "example";
				request.SellerId = sellerId;
				string mwsAuthToken = "example";
				request.MWSAuthToken = mwsAuthToken;
				string marketplaceId = "example";
				request.MarketplaceId = marketplaceId;
				string asin = "example";
				request.ASIN = asin;
				string itemCondition = "example";
				request.ItemCondition = itemCondition;
				return this.client.GetLowestPricedOffersForASIN(request);
			}

			public GetLowestPricedOffersForSKUResponse InvokeGetLowestPricedOffersForSKU()
			{
				// Create a request.
				GetLowestPricedOffersForSKURequest request = new GetLowestPricedOffersForSKURequest();
				string sellerId = "example";
				request.SellerId = sellerId;
				string mwsAuthToken = "example";
				request.MWSAuthToken = mwsAuthToken;
				string marketplaceId = "example";
				request.MarketplaceId = marketplaceId;
				string sellerSKU = "example";
				request.SellerSKU = sellerSKU;
				string itemCondition = "example";
				request.ItemCondition = itemCondition;
				return this.client.GetLowestPricedOffersForSKU(request);
			}

			public GetMatchingProductResponse InvokeGetMatchingProduct()
			{
				// Create a request.
				GetMatchingProductRequest request = new GetMatchingProductRequest();
				string sellerId = "example";
				request.SellerId = sellerId;
				string mwsAuthToken = "example";
				request.MWSAuthToken = mwsAuthToken;
				string marketplaceId = "example";
				request.MarketplaceId = marketplaceId;
				ASINListType asinList = new ASINListType();
				request.ASINList = asinList;
				return this.client.GetMatchingProduct(request);
			}

			public GetMatchingProductForIdResponse InvokeGetMatchingProductForId()
			{
				// Create a request.
				GetMatchingProductForIdRequest request = new GetMatchingProductForIdRequest();
				string sellerId = "example";
				request.SellerId = sellerId;
				string mwsAuthToken = "example";
				request.MWSAuthToken = mwsAuthToken;
				string marketplaceId = "example";
				request.MarketplaceId = marketplaceId;
				string idType = "example";
				request.IdType = idType;
				IdListType idList = new IdListType();
				request.IdList = idList;
				return this.client.GetMatchingProductForId(request);
			}

			public GetMyFeesEstimateResponse InvokeGetMyFeesEstimate()
			{
				// Create a request.
				GetMyFeesEstimateRequest request = new GetMyFeesEstimateRequest();
				string sellerId = "example";
				request.SellerId = sellerId;
				string mwsAuthToken = "example";
				request.MWSAuthToken = mwsAuthToken;
				FeesEstimateRequestList feesEstimateRequestList = new FeesEstimateRequestList();
				request.FeesEstimateRequestList = feesEstimateRequestList;
				return this.client.GetMyFeesEstimate(request);
			}

			public GetMyPriceForASINResponse InvokeGetMyPriceForASIN()
			{
				// Create a request.
				GetMyPriceForASINRequest request = new GetMyPriceForASINRequest();
				string sellerId = "ARA1ZW7ZHL5MQ";
				request.SellerId = sellerId;
				string mwsAuthToken = "example";
				request.MWSAuthToken = mwsAuthToken;
				string marketplaceId = "ATVPDKIKX0DER";
				request.MarketplaceId = marketplaceId;
				ASINListType asinList = new ASINListType();
				request.ASINList = asinList;
				return this.client.GetMyPriceForASIN(request);
			}

			public GetMyPriceForSKUResponse InvokeGetMyPriceForSKU()
			{
				// Create a request.
				GetMyPriceForSKURequest request = new GetMyPriceForSKURequest();
				string sellerId = "example";
				request.SellerId = sellerId;
				string mwsAuthToken = "example";
				request.MWSAuthToken = mwsAuthToken;
				string marketplaceId = "example";
				request.MarketplaceId = marketplaceId;
				SellerSKUListType sellerSKUList = new SellerSKUListType();
				request.SellerSKUList = sellerSKUList;
				return this.client.GetMyPriceForSKU(request);
			}

			public GetProductCategoriesForASINResponse InvokeGetProductCategoriesForASIN()
			{
				// Create a request.
				GetProductCategoriesForASINRequest request = new GetProductCategoriesForASINRequest();
				string sellerId = "example";
				request.SellerId = sellerId;
				string mwsAuthToken = "example";
				request.MWSAuthToken = mwsAuthToken;
				string marketplaceId = "example";
				request.MarketplaceId = marketplaceId;
				string asin = "example";
				request.ASIN = asin;
				return this.client.GetProductCategoriesForASIN(request);
			}

			public GetProductCategoriesForSKUResponse InvokeGetProductCategoriesForSKU()
			{
				// Create a request.
				GetProductCategoriesForSKURequest request = new GetProductCategoriesForSKURequest();
				string sellerId = "example";
				request.SellerId = sellerId;
				string mwsAuthToken = "example";
				request.MWSAuthToken = mwsAuthToken;
				string marketplaceId = "example";
				request.MarketplaceId = marketplaceId;
				string sellerSKU = "example";
				request.SellerSKU = sellerSKU;
				return this.client.GetProductCategoriesForSKU(request);
			}

			public GetServiceStatusResponse InvokeGetServiceStatus()
			{
				// Create a request.
				GetServiceStatusRequest request = new GetServiceStatusRequest();
				string sellerId = "example";
				request.SellerId = sellerId;
				string mwsAuthToken = "example";
				request.MWSAuthToken = mwsAuthToken;
				return this.client.GetServiceStatus(request);
			}

			public ListMatchingProductsResponse InvokeListMatchingProducts()
			{
				// Create a request.
				ListMatchingProductsRequest request = new ListMatchingProductsRequest();
				string sellerId = "example";
				request.SellerId = sellerId;
				string mwsAuthToken = "example";
				request.MWSAuthToken = mwsAuthToken;
				string marketplaceId = "example";
				request.MarketplaceId = marketplaceId;
				string query = "example";
				request.Query = query;
				string queryContextId = "example";
				request.QueryContextId = queryContextId;
				return this.client.ListMatchingProducts(request);
			}


		}
		//	public class MWSSAMPLE
		//   {
		//	public static void Test()
		//	{
		//		// TODO: Set the below configuration variables before attempting to run
		//		//Developer ID: 760245788124
		//		//AWS Access Key ID: AKIAI5X4MRMW7C6MUYZA
		//		//Secret Key: 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

		//		//Current Marketplace: ATVPDKIKX0DER
		//		//Seller ID: ARA1ZW7ZHL5MQ

		//		// Developer AWS access key
		//		string accessKey = "AKIAJZY7ZVPLTWQYHWYA";

		//		// Developer AWS secret key
		//		string secretKey = "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";

		//		// The client application name
		//		string appName = "CSharpSampleCode";

		//		// The client application version
		//		string appVersion = "1.0";

		//		// The endpoint for region service and version (see developer guide)
		//		// ex: https://mws.amazonservices.com
		//		string serviceURL = "https://mws.amazonservices.com";

		//		// Create a configuration object
		//		MarketplaceWebServiceProductsConfig config = new MarketplaceWebServiceProductsConfig();
		//		config.ServiceURL = serviceURL;

		//		// Set other client connection configurations here if needed
		//		// Create the client itself
		//		MarketplaceWebServiceProductsClient client = new MarketplaceWebServiceProductsClient(appName, appVersion, accessKey, secretKey, config);

		//		MarketplaceWebServiceProductsSample sample = new MarketplaceWebServiceProductsSample(client);
		//		IMWSResponse response = null;

		//		//response = sample.InvokeGetCompetitivePricingForASIN();
		//		// response = sample.InvokeGetCompetitivePricingForSKU();
		//		// response = sample.InvokeGetLowestOfferListingsForASIN();
		//		// response = sample.InvokeGetLowestOfferListingsForSKU();
		//		// response = sample.InvokeGetLowestPricedOffersForASIN();
		//		// response = sample.InvokeGetLowestPricedOffersForSKU();
		//		// response = sample.InvokeGetMatchingProduct();
		//		// response = sample.InvokeGetMatchingProductForId();
		//		// response = sample.InvokeGetMyFeesEstimate();
		//		// response = sample.InvokeGetMyPriceForASIN();
		//		  response = sample.InvokeGetMyPriceForSKU() as IMWSResponse;
		//		// response = sample.InvokeGetProductCategoriesForASIN();
		//		// response = sample.InvokeGetProductCategoriesForSKU();
		//		// response = sample.InvokeGetServiceStatus();
		//		// response = sample.InvokeListMatchingProducts();
		//		Console.WriteLine("Response:");
		//		ResponseHeaderMetadata rhmd = response.ResponseHeaderMetadata;
		//		// We recommend logging the request id and timestamp of every call.
		//		Console.WriteLine("RequestId: " + rhmd.RequestId);
		//		Console.WriteLine("Timestamp: " + rhmd.Timestamp);
		//		string responseXml = response.ToXML();
		//		Console.WriteLine(responseXml);
		//	}
		//	private readonly MarketplaceWebServiceProducts client;

		//	public MarketplaceWebServiceProductsSample(MarketplaceWebServiceProducts client)
		//	{
		//		this.client = client;
		//	}

		//	public GetCompetitivePricingForASINResponse InvokeGetCompetitivePricingForASIN()
		//	{
		//		// Create a request.
		//		GetCompetitivePricingForASINRequest request = new GetCompetitivePricingForASINRequest();
		//		string sellerId = "example";
		//		request.SellerId = sellerId;
		//		string mwsAuthToken = "example";
		//		request.MWSAuthToken = mwsAuthToken;
		//		string marketplaceId = "example";
		//		request.MarketplaceId = marketplaceId;
		//		ASINListType asinList = new ASINListType();
		//		request.ASINList = asinList;
		//		return this.client.GetCompetitivePricingForASIN(request);
		//	}

		//	public GetCompetitivePricingForSKUResponse InvokeGetCompetitivePricingForSKU()
		//	{
		//		// Create a request.
		//		GetCompetitivePricingForSKURequest request = new GetCompetitivePricingForSKURequest();
		//		string sellerId = "example";
		//		request.SellerId = sellerId;
		//		string mwsAuthToken = "example";
		//		request.MWSAuthToken = mwsAuthToken;
		//		string marketplaceId = "example";
		//		request.MarketplaceId = marketplaceId;
		//		SellerSKUListType sellerSKUList = new SellerSKUListType();
		//		request.SellerSKUList = sellerSKUList;
		//		return this.client.GetCompetitivePricingForSKU(request);
		//	}

		//	public GetLowestOfferListingsForASINResponse InvokeGetLowestOfferListingsForASIN()
		//	{
		//		// Create a request.
		//		GetLowestOfferListingsForASINRequest request = new GetLowestOfferListingsForASINRequest();
		//		string sellerId = "example";
		//		request.SellerId = sellerId;
		//		string mwsAuthToken = "example";
		//		request.MWSAuthToken = mwsAuthToken;
		//		string marketplaceId = "example";
		//		request.MarketplaceId = marketplaceId;
		//		ASINListType asinList = new ASINListType();
		//		request.ASINList = asinList;
		//		string itemCondition = "example";
		//		request.ItemCondition = itemCondition;
		//		bool excludeMe = true;
		//		request.ExcludeMe = excludeMe;
		//		return this.client.GetLowestOfferListingsForASIN(request);
		//	}

		//	public GetLowestOfferListingsForSKUResponse InvokeGetLowestOfferListingsForSKU()
		//	{
		//		// Create a request.
		//		GetLowestOfferListingsForSKURequest request = new GetLowestOfferListingsForSKURequest();
		//		string sellerId = "example";
		//		request.SellerId = sellerId;
		//		string mwsAuthToken = "example";
		//		request.MWSAuthToken = mwsAuthToken;
		//		string marketplaceId = "example";
		//		request.MarketplaceId = marketplaceId;
		//		SellerSKUListType sellerSKUList = new SellerSKUListType();
		//		request.SellerSKUList = sellerSKUList;
		//		string itemCondition = "example";
		//		request.ItemCondition = itemCondition;
		//		bool excludeMe = true;
		//		request.ExcludeMe = excludeMe;
		//		return this.client.GetLowestOfferListingsForSKU(request);
		//	}

		//	public GetLowestPricedOffersForASINResponse InvokeGetLowestPricedOffersForASIN()
		//	{
		//		// Create a request.
		//		GetLowestPricedOffersForASINRequest request = new GetLowestPricedOffersForASINRequest();
		//		string sellerId = "example";
		//		request.SellerId = sellerId;
		//		string mwsAuthToken = "example";
		//		request.MWSAuthToken = mwsAuthToken;
		//		string marketplaceId = "example";
		//		request.MarketplaceId = marketplaceId;
		//		string asin = "example";
		//		request.ASIN = asin;
		//		string itemCondition = "example";
		//		request.ItemCondition = itemCondition;
		//		return this.client.GetLowestPricedOffersForASIN(request);
		//	}

		//	public GetLowestPricedOffersForSKUResponse InvokeGetLowestPricedOffersForSKU()
		//	{
		//		// Create a request.
		//		GetLowestPricedOffersForSKURequest request = new GetLowestPricedOffersForSKURequest();
		//		string sellerId = "example";
		//		request.SellerId = sellerId;
		//		string mwsAuthToken = "example";
		//		request.MWSAuthToken = mwsAuthToken;
		//		string marketplaceId = "example";
		//		request.MarketplaceId = marketplaceId;
		//		string sellerSKU = "example";
		//		request.SellerSKU = sellerSKU;
		//		string itemCondition = "example";
		//		request.ItemCondition = itemCondition;
		//		return this.client.GetLowestPricedOffersForSKU(request);
		//	}

		//	public GetMatchingProductResponse InvokeGetMatchingProduct()
		//	{
		//		// Create a request.
		//		GetMatchingProductRequest request = new GetMatchingProductRequest();
		//		string sellerId = "example";
		//		request.SellerId = sellerId;
		//		string mwsAuthToken = "example";
		//		request.MWSAuthToken = mwsAuthToken;
		//		string marketplaceId = "example";
		//		request.MarketplaceId = marketplaceId;
		//		ASINListType asinList = new ASINListType();
		//		request.ASINList = asinList;
		//		return this.client.GetMatchingProduct(request);
		//	}

		//	public GetMatchingProductForIdResponse InvokeGetMatchingProductForId()
		//	{
		//		// Create a request.
		//		GetMatchingProductForIdRequest request = new GetMatchingProductForIdRequest();
		//		string sellerId = "example";
		//		request.SellerId = sellerId;
		//		string mwsAuthToken = "example";
		//		request.MWSAuthToken = mwsAuthToken;
		//		string marketplaceId = "example";
		//		request.MarketplaceId = marketplaceId;
		//		string idType = "example";
		//		request.IdType = idType;
		//		IdListType idList = new IdListType();
		//		request.IdList = idList;
		//		return this.client.GetMatchingProductForId(request);
		//	}

		//	public GetMyFeesEstimateResponse InvokeGetMyFeesEstimate()
		//	{
		//		// Create a request.
		//		GetMyFeesEstimateRequest request = new GetMyFeesEstimateRequest();
		//		string sellerId = "example";
		//		request.SellerId = sellerId;
		//		string mwsAuthToken = "example";
		//		request.MWSAuthToken = mwsAuthToken;
		//		FeesEstimateRequestList feesEstimateRequestList = new FeesEstimateRequestList();
		//		request.FeesEstimateRequestList = feesEstimateRequestList;
		//		return this.client.GetMyFeesEstimate(request);
		//	}

		//	public GetMyPriceForASINResponse InvokeGetMyPriceForASIN()
		//	{
		//		// Create a request.
		//		GetMyPriceForASINRequest request = new GetMyPriceForASINRequest();
		//		string sellerId = "example";
		//		request.SellerId = sellerId;
		//		string mwsAuthToken = "example";
		//		request.MWSAuthToken = mwsAuthToken;
		//		string marketplaceId = "example";
		//		request.MarketplaceId = marketplaceId;
		//		ASINListType asinList = new ASINListType();
		//		request.ASINList = asinList;
		//		return this.client.GetMyPriceForASIN(request);
		//	}

		//	public GetMyPriceForSKUResponse InvokeGetMyPriceForSKU()
		//	{
		//		// Create a request.
		//		GetMyPriceForSKURequest request = new GetMyPriceForSKURequest();
		//		string sellerId = "example";
		//		request.SellerId = sellerId;
		//		string mwsAuthToken = "example";
		//		request.MWSAuthToken = mwsAuthToken;
		//		string marketplaceId = "example";
		//		request.MarketplaceId = marketplaceId;
		//		SellerSKUListType sellerSKUList = new SellerSKUListType();
		//		request.SellerSKUList = sellerSKUList;
		//		return this.client.GetMyPriceForSKU(request);
		//	}

		//	public GetProductCategoriesForASINResponse InvokeGetProductCategoriesForASIN()
		//	{
		//		// Create a request.
		//		GetProductCategoriesForASINRequest request = new GetProductCategoriesForASINRequest();
		//		string sellerId = "example";
		//		request.SellerId = sellerId;
		//		string mwsAuthToken = "example";
		//		request.MWSAuthToken = mwsAuthToken;
		//		string marketplaceId = "example";
		//		request.MarketplaceId = marketplaceId;
		//		string asin = "example";
		//		request.ASIN = asin;
		//		return this.client.GetProductCategoriesForASIN(request);
		//	}

		//	public GetProductCategoriesForSKUResponse InvokeGetProductCategoriesForSKU()
		//	{
		//		// Create a request.
		//		GetProductCategoriesForSKURequest request = new GetProductCategoriesForSKURequest();
		//		string sellerId = "example";
		//		request.SellerId = sellerId;
		//		string mwsAuthToken = "example";
		//		request.MWSAuthToken = mwsAuthToken;
		//		string marketplaceId = "example";
		//		request.MarketplaceId = marketplaceId;
		//		string sellerSKU = "example";
		//		request.SellerSKU = sellerSKU;
		//		return this.client.GetProductCategoriesForSKU(request);
		//	}

		//	public GetServiceStatusResponse InvokeGetServiceStatus()
		//	{
		//		// Create a request.
		//		GetServiceStatusRequest request = new GetServiceStatusRequest();
		//		string sellerId = "example";
		//		request.SellerId = sellerId;
		//		string mwsAuthToken = "example";
		//		request.MWSAuthToken = mwsAuthToken;
		//		return this.client.GetServiceStatus(request);
		//	}

		//	public ListMatchingProductsResponse InvokeListMatchingProducts()
		//	{
		//		// Create a request.
		//		ListMatchingProductsRequest request = new ListMatchingProductsRequest();
		//		string sellerId = "example";
		//		request.SellerId = sellerId;
		//		string mwsAuthToken = "example";
		//		request.MWSAuthToken = mwsAuthToken;
		//		string marketplaceId = "example";
		//		request.MarketplaceId = marketplaceId;
		//		string query = "example";
		//		request.Query = query;
		//		string queryContextId = "example";
		//		request.QueryContextId = queryContextId;
		//		return this.client.ListMatchingProducts(request);
		//	}


		//}
	}
}
