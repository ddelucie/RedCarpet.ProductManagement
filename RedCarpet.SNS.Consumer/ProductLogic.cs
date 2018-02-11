using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedCarpet.SNS.Consumer
{
	public class PricingResult
	{
		public decimal NewPrice { get; set; }
		public decimal OriginalPrice { get; set; }
		public bool PriceChanged
		{
			get
			{
				return NewPrice != OriginalPrice;
			}
		}

	}


	public enum PriceCategory { Min, Max, BuyBox }

	public static class ProductLogic
	{
		public static PricingResult SetPrice(Notification notification, dynamic product)
		{
			PricingResult pricingResult = new PricingResult();
			decimal buyBoxPrice = FindBuyBoxPrice(notification, product);
			PriceCategory priceCategory = FindPriceCategory(buyBoxPrice, product);
			decimal setPrice = product.MaxPrice;
			if (priceCategory.Equals(PriceCategory.BuyBox)) setPrice = buyBoxPrice;
			if (priceCategory.Equals(PriceCategory.Min)) setPrice = product.MinPrice;

			pricingResult.OriginalPrice = product.CurrentPrice;
			pricingResult.NewPrice = setPrice;

			return pricingResult;
		}

		public static PriceCategory FindPriceCategory(decimal landedPrice, dynamic product)
		{
			PriceCategory price = PriceCategory.Max;
			if (landedPrice == 0m) price = PriceCategory.Max;
			if (landedPrice < product.MinPrice) price = PriceCategory.Min;
			else price = PriceCategory.BuyBox;

			return price;
		}

		public static decimal FindBuyBoxPrice(Notification notification, dynamic product)
		{
			decimal buyBoxPrice = 0m;
			if (notification == null) return buyBoxPrice;
			if (!decimal.TryParse(notification.NotificationPayload.AnyOfferChangedNotification.Summary.BuyBoxPrices.BuyBoxPrice.LandedPrice.Amount, out buyBoxPrice))
			{ buyBoxPrice = 0m; }
			return buyBoxPrice;
		}



	}
}
