using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace RedCarpet.SNS.Consumer
{
	[XmlRoot(ElementName = "NotificationMetaData")]
	public class NotificationMetaData
	{
		[XmlElement(ElementName = "NotificationType")]
		public string NotificationType { get; set; }
		[XmlElement(ElementName = "PayloadVersion")]
		public string PayloadVersion { get; set; }
		[XmlElement(ElementName = "UniqueId")]
		public string UniqueId { get; set; }
		[XmlElement(ElementName = "PublishTime")]
		public string PublishTime { get; set; }
		[XmlElement(ElementName = "SellerId")]
		public string SellerId { get; set; }
		[XmlElement(ElementName = "MarketplaceId")]
		public string MarketplaceId { get; set; }
	}

	[XmlRoot(ElementName = "OfferChangeTrigger")]
	public class OfferChangeTrigger
	{
		[XmlElement(ElementName = "MarketplaceId")]
		public string MarketplaceId { get; set; }
		[XmlElement(ElementName = "ASIN")]
		public string ASIN { get; set; }
		[XmlElement(ElementName = "ItemCondition")]
		public string ItemCondition { get; set; }
		[XmlElement(ElementName = "TimeOfOfferChange")]
		public string TimeOfOfferChange { get; set; }
	}

	[XmlRoot(ElementName = "OfferCount")]
	public class OfferCount
	{
		[XmlAttribute(AttributeName = "condition")]
		public string Condition { get; set; }
		[XmlAttribute(AttributeName = "fulfillmentChannel")]
		public string FulfillmentChannel { get; set; }
		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "NumberOfOffers")]
	public class NumberOfOffers
	{
		[XmlElement(ElementName = "OfferCount")]
		public List<OfferCount> OfferCount { get; set; }
	}

	[XmlRoot(ElementName = "LandedPrice")]
	public class LandedPrice
	{
		[XmlElement(ElementName = "Amount")]
		public string Amount { get; set; }
		[XmlElement(ElementName = "CurrencyCode")]
		public string CurrencyCode { get; set; }
	}

	[XmlRoot(ElementName = "ListingPrice")]
	public class ListingPrice
	{
		[XmlElement(ElementName = "Amount")]
		public string Amount { get; set; }
		[XmlElement(ElementName = "CurrencyCode")]
		public string CurrencyCode { get; set; }
	}

	[XmlRoot(ElementName = "Shipping")]
	public class Shipping
	{
		[XmlElement(ElementName = "Amount")]
		public string Amount { get; set; }
		[XmlElement(ElementName = "CurrencyCode")]
		public string CurrencyCode { get; set; }
	}

	[XmlRoot(ElementName = "LowestPrice")]
	public class LowestPrice
	{
		[XmlElement(ElementName = "LandedPrice")]
		public LandedPrice LandedPrice { get; set; }
		[XmlElement(ElementName = "ListingPrice")]
		public ListingPrice ListingPrice { get; set; }
		[XmlElement(ElementName = "Shipping")]
		public Shipping Shipping { get; set; }
		[XmlAttribute(AttributeName = "condition")]
		public string Condition { get; set; }
		[XmlAttribute(AttributeName = "fulfillmentChannel")]
		public string FulfillmentChannel { get; set; }
	}

	[XmlRoot(ElementName = "LowestPrices")]
	public class LowestPrices
	{
		[XmlElement(ElementName = "LowestPrice")]
		public List<LowestPrice> LowestPrice { get; set; }
	}

	[XmlRoot(ElementName = "BuyBoxPrice")]
	public class BuyBoxPrice
	{
		[XmlElement(ElementName = "LandedPrice")]
		public LandedPrice LandedPrice { get; set; }
		[XmlElement(ElementName = "ListingPrice")]
		public ListingPrice ListingPrice { get; set; }
		[XmlElement(ElementName = "Shipping")]
		public Shipping Shipping { get; set; }
		[XmlAttribute(AttributeName = "condition")]
		public string Condition { get; set; }
	}

	[XmlRoot(ElementName = "BuyBoxPrices")]
	public class BuyBoxPrices
	{
		[XmlElement(ElementName = "BuyBoxPrice")]
		public BuyBoxPrice BuyBoxPrice { get; set; }
	}

	[XmlRoot(ElementName = "ListPrice")]
	public class ListPrice
	{
		[XmlElement(ElementName = "Amount")]
		public string Amount { get; set; }
		[XmlElement(ElementName = "CurrencyCode")]
		public string CurrencyCode { get; set; }
	}

	[XmlRoot(ElementName = "SuggestedLowerPricePlusShipping")]
	public class SuggestedLowerPricePlusShipping
	{
		[XmlElement(ElementName = "Amount")]
		public string Amount { get; set; }
		[XmlElement(ElementName = "CurrencyCode")]
		public string CurrencyCode { get; set; }
	}

	[XmlRoot(ElementName = "SalesRank")]
	public class SalesRank
	{
		[XmlElement(ElementName = "ProductCategoryId")]
		public string ProductCategoryId { get; set; }
		[XmlElement(ElementName = "Rank")]
		public string Rank { get; set; }
	}

	[XmlRoot(ElementName = "SalesRankings")]
	public class SalesRankings
	{
		[XmlElement(ElementName = "SalesRank")]
		public List<SalesRank> SalesRank { get; set; }
	}

	[XmlRoot(ElementName = "BuyBoxEligibleOffers")]
	public class BuyBoxEligibleOffers
	{
		[XmlElement(ElementName = "OfferCount")]
		public List<OfferCount> OfferCount { get; set; }
	}

	[XmlRoot(ElementName = "Summary")]
	public class Summary
	{
		[XmlElement(ElementName = "NumberOfOffers")]
		public NumberOfOffers NumberOfOffers { get; set; }
		[XmlElement(ElementName = "LowestPrices")]
		public LowestPrices LowestPrices { get; set; }
		[XmlElement(ElementName = "BuyBoxPrices")]
		public BuyBoxPrices BuyBoxPrices { get; set; }
		[XmlElement(ElementName = "ListPrice")]
		public ListPrice ListPrice { get; set; }
		[XmlElement(ElementName = "SuggestedLowerPricePlusShipping")]
		public SuggestedLowerPricePlusShipping SuggestedLowerPricePlusShipping { get; set; }
		[XmlElement(ElementName = "SalesRankings")]
		public SalesRankings SalesRankings { get; set; }
		[XmlElement(ElementName = "BuyBoxEligibleOffers")]
		public BuyBoxEligibleOffers BuyBoxEligibleOffers { get; set; }
	}

	[XmlRoot(ElementName = "SellerFeedbackRating")]
	public class SellerFeedbackRating
	{
		[XmlElement(ElementName = "SellerPositiveFeedbackRating")]
		public string SellerPositiveFeedbackRating { get; set; }
		[XmlElement(ElementName = "FeedbackCount")]
		public string FeedbackCount { get; set; }
	}

	[XmlRoot(ElementName = "ShippingTime")]
	public class ShippingTime
	{
		[XmlAttribute(AttributeName = "minimumHours")]
		public string MinimumHours { get; set; }
		[XmlAttribute(AttributeName = "maximumHours")]
		public string MaximumHours { get; set; }
		[XmlAttribute(AttributeName = "availabilityType")]
		public string AvailabilityType { get; set; }
	}

	[XmlRoot(ElementName = "Offer")]
	public class Offer
	{
		[XmlElement(ElementName = "SellerId")]
		public string SellerId { get; set; }
		[XmlElement(ElementName = "SubCondition")]
		public string SubCondition { get; set; }
		[XmlElement(ElementName = "SellerFeedbackRating")]
		public SellerFeedbackRating SellerFeedbackRating { get; set; }
		[XmlElement(ElementName = "ShippingTime")]
		public ShippingTime ShippingTime { get; set; }
		[XmlElement(ElementName = "ListingPrice")]
		public ListingPrice ListingPrice { get; set; }
		[XmlElement(ElementName = "Shipping")]
		public Shipping Shipping { get; set; }
		[XmlElement(ElementName = "IsFulfilledByAmazon")]
		public string IsFulfilledByAmazon { get; set; }
		[XmlElement(ElementName = "IsBuyBoxWinner")]
		public string IsBuyBoxWinner { get; set; }
		[XmlElement(ElementName = "IsFeaturedMerchant")]
		public string IsFeaturedMerchant { get; set; }
		[XmlElement(ElementName = "ShipsDomestically")]
		public string ShipsDomestically { get; set; }
		[XmlElement(ElementName = "ShipsFrom")]
		public ShipsFrom ShipsFrom { get; set; }
	}

	[XmlRoot(ElementName = "ShipsFrom")]
	public class ShipsFrom
	{
		[XmlElement(ElementName = "Country")]
		public string Country { get; set; }
		[XmlElement(ElementName = "State")]
		public string State { get; set; }
	}

	[XmlRoot(ElementName = "Offers")]
	public class Offers
	{
		[XmlElement(ElementName = "Offer")]
		public List<Offer> Offer { get; set; }
	}

	[XmlRoot(ElementName = "AnyOfferChangedNotification")]
	public class AnyOfferChangedNotification
	{
		[XmlElement(ElementName = "OfferChangeTrigger")]
		public OfferChangeTrigger OfferChangeTrigger { get; set; }
		[XmlElement(ElementName = "Summary")]
		public Summary Summary { get; set; }
		[XmlElement(ElementName = "Offers")]
		public Offers Offers { get; set; }
	}

	[XmlRoot(ElementName = "NotificationPayload")]
	public class NotificationPayload
	{
		[XmlElement(ElementName = "AnyOfferChangedNotification")]
		public AnyOfferChangedNotification AnyOfferChangedNotification { get; set; }
	}

	[XmlRoot(ElementName = "Notification")]
	public class Notification
	{
		[XmlElement(ElementName = "NotificationMetaData")]
		public NotificationMetaData NotificationMetaData { get; set; }
		[XmlElement(ElementName = "NotificationPayload")]
		public NotificationPayload NotificationPayload { get; set; }
	}

}
