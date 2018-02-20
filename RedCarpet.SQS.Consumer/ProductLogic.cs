using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedCarpet.Data.Model;

namespace RedCarpet.SQS.Consumer
{
	//public class PricingResult
	//{
	//	public decimal NewPrice { get; set; }
	//	public decimal OriginalPrice { get; set; }
	//	public bool PriceChanged
	//	{
	//		get
	//		{
	//			return NewPrice != OriginalPrice;
	//		}
	//	}

	//}



	public static class ProductLogic
	{
		public static PricingResult SetPrice(Notification notification, Product product)
		{
			PricingResult pricingResult = BuildPricingResult(notification);
			decimal buyBoxPrice = pricingResult.LandedPrice; // Using LandedPrice

			// populate Product values 
			pricingResult.MaxPrice = product.MaxAmazonSellPrice;
			pricingResult.MinPrice = product.MinAmazonSellPrice;
			pricingResult.OriginalPrice = product.CurrentPrice;

			pricingResult.PriceCategorySelected = FindPriceCategory(buyBoxPrice, product);
			decimal setPrice = pricingResult.MaxPrice;
			if (pricingResult.PriceCategorySelected.Equals(PriceCategory.BuyBox)) setPrice = buyBoxPrice;
			if (pricingResult.PriceCategorySelected.Equals(PriceCategory.Min)) setPrice = pricingResult.MinPrice;

			pricingResult.NewPrice = setPrice;

			return pricingResult;
		}

		private static string FindPriceCategory(decimal landedPrice, Product product)
		{
			string price = PriceCategory.Max;
			if (landedPrice == 0m) price = PriceCategory.Max;
			if (landedPrice < product.MinAmazonSellPrice) price = PriceCategory.Min;
			else price = PriceCategory.BuyBox;

			return price;
		}


		public static PricingResult BuildPricingResult(Notification notification)
		{
			PricingResult pricingResult = new PricingResult();

			if (notification == null) return pricingResult;

			// offer data
			pricingResult.ASIN = notification.NotificationPayload.AnyOfferChangedNotification.OfferChangeTrigger.ASIN;
			DateTime timeOfOfferChange = DateTime.UtcNow;
			if (DateTime.TryParse(notification.NotificationPayload.AnyOfferChangedNotification.OfferChangeTrigger.TimeOfOfferChange, out timeOfOfferChange))
				pricingResult.TimeOfOfferChange = timeOfOfferChange;


			pricingResult.LandedPrice = GetLandedPrice(notification);
			pricingResult.ListingPrice = GetListingPrice(notification);

			if (pricingResult.LandedPrice == 0.0m)
				pricingResult.LandedPrice = GetLowestOfferPrice(notification);


			return pricingResult;
		}


		public static decimal GetLowestOfferPrice(Notification notification)
		{
			if (notification.NotificationPayload.AnyOfferChangedNotification.Offers == null) return 0.0m;
			if (notification.NotificationPayload.AnyOfferChangedNotification.Offers.Offer == null) return 0.0m;

			var offerPrices = notification.NotificationPayload.AnyOfferChangedNotification.Offers.Offer
				.Select(o => new { OfferPrice = o.ListingPrice.Amount, Shipping = o.Shipping.Amount });
			IList<decimal> prices = new List<decimal>();

			foreach (var offerPrice in offerPrices)
			{
				decimal offer; decimal ship;
				if (!decimal.TryParse(offerPrice.OfferPrice, out offer)) offer = 0.0m;
				if (!decimal.TryParse(offerPrice.Shipping, out ship)) ship = 0.0m;

				prices.Add(offer + ship);
			}

			return prices.Min(p => p);
		}


		public static decimal GetLandedPrice(Notification notification)
		{           

			if (notification.NotificationPayload.AnyOfferChangedNotification.Summary == null) return 0.0m;
			if (notification.NotificationPayload.AnyOfferChangedNotification.Summary.BuyBoxPrices == null) return 0.0m;

			decimal price = 0m;
			if (!decimal.TryParse(notification.NotificationPayload.AnyOfferChangedNotification.Summary.BuyBoxPrices.BuyBoxPrice.LandedPrice.Amount, out price))
			{ price = 0m; }
			return price;
		}
		public static decimal GetListingPrice(Notification notification)
		{

			if (notification.NotificationPayload.AnyOfferChangedNotification.Summary == null) return 0.0m;
			if (notification.NotificationPayload.AnyOfferChangedNotification.Summary.BuyBoxPrices == null) return 0.0m;

			decimal price = 0m;
			if (!decimal.TryParse(notification.NotificationPayload.AnyOfferChangedNotification.Summary.BuyBoxPrices.BuyBoxPrice.ListingPrice.Amount, out price))
			{ price = 0m; }
			return price;
		}
	}
}
