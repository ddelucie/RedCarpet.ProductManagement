using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedCarpet.Data.Model
{
	//public enum PriceCategory { Min, Max, BuyBox }
	public enum Provider { Amazon }

	public static class PriceCategory
	{
		public const string None = null;
		public const string Min = "Min";
		public const string Max = "Max";
		public const string BuyBox = "BuyBox";
	}

	public class PricingResult
	{
		[Key]
		public long PricingResultHistoryId { get; set; }
		public decimal NewPrice { get; set; }
		public decimal OriginalPrice { get; set; }
		public decimal LandedPrice { get; set; }
		public decimal ListingPrice { get; set; }
		public decimal MaxPrice { get; set; }
		public decimal MinPrice { get; set; }
		public string PriceCategorySelected { get; set; }
		public DateTime TimeOfOfferChange { get; set; }
		public string ASIN { get; set; }

		public bool IsPriceChanged
		{
			get
			{
				return NewPrice - OriginalPrice >= 0.01m;
			}
			set { }
		}
		public DateTime DateEntry { get; set; }

		public bool PriceUpdateSucceeded { get; set; }

	}
}
