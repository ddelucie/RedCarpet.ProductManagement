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
		public static PricingResult SetPrice(Notification notification, dynamic product)
		{
			PricingResult pricingResult = BuildPricingResult(notification);
			decimal buyBoxPrice = pricingResult.LandedPrice; // Using LandedPrice

			// populate Product values // TODO:  real product
			pricingResult.MaxPrice = product.MaxPrice;
			pricingResult.MinPrice = product.MinPrice;
			pricingResult.OriginalPrice = product.CurrentPrice;

			pricingResult.PriceCategorySelected = FindPriceCategory(buyBoxPrice, product);
			decimal setPrice = pricingResult.MaxPrice;
			if (pricingResult.PriceCategorySelected.Equals(PriceCategory.BuyBox)) setPrice = buyBoxPrice;
			if (pricingResult.PriceCategorySelected.Equals(PriceCategory.Min)) setPrice = pricingResult.MinPrice;

			pricingResult.NewPrice = setPrice;

			return pricingResult;
		}

		private static string FindPriceCategory(decimal landedPrice, dynamic product)
		{
			string price = PriceCategory.Max;
			if (landedPrice == 0m) price = PriceCategory.Max;
			if (landedPrice < product.MinPrice) price = PriceCategory.Min;
			else price = PriceCategory.BuyBox;

			return price;
		}

		//private static decimal FindBuyBoxPrice(Notification notification, dynamic product)
		//{
		//	decimal buyBoxPrice = 0m;
		//	if (notification == null) return buyBoxPrice;
		//	if (!decimal.TryParse(notification.NotificationPayload.AnyOfferChangedNotification.Summary.BuyBoxPrices.BuyBoxPrice.LandedPrice.Amount, out buyBoxPrice))
		//	{ buyBoxPrice = 0m; }
		//	return buyBoxPrice;
		//}

		public static PricingResult BuildPricingResult(Notification notification)
		{
			PricingResult pricingResult = new PricingResult();

			if (notification == null) return pricingResult;

			// offer data
			pricingResult.ASIN = notification.NotificationPayload.AnyOfferChangedNotification.OfferChangeTrigger.ASIN;
			DateTime timeOfOfferChange = DateTime.UtcNow;
			if (DateTime.TryParse(notification.NotificationPayload.AnyOfferChangedNotification.OfferChangeTrigger.TimeOfOfferChange, out timeOfOfferChange))
				pricingResult.TimeOfOfferChange = timeOfOfferChange;

			// buy box prices
			if (notification.NotificationPayload.AnyOfferChangedNotification.Summary == null) return pricingResult;
			if (notification.NotificationPayload.AnyOfferChangedNotification.Summary.BuyBoxPrices == null) return pricingResult;

			decimal landedPrice = 0m;
			if (!decimal.TryParse(notification.NotificationPayload.AnyOfferChangedNotification.Summary.BuyBoxPrices.BuyBoxPrice.LandedPrice.Amount, out landedPrice))
			{ landedPrice = 0m; }
			pricingResult.LandedPrice = landedPrice;

			decimal listingPrice = 0m;
			if (!decimal.TryParse(notification.NotificationPayload.AnyOfferChangedNotification.Summary.BuyBoxPrices.BuyBoxPrice.ListingPrice.Amount, out listingPrice))
			{ listingPrice = 0m; }
			pricingResult.ListingPrice = listingPrice;

			return pricingResult;
		}


	}
}
