using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedCarpet.Data.Model
{
	public class Product
	{
		[Key]
		public int ProductId { get; set; }
		public string ItemNumber { get; set; }

		public string ASIN { get; set; }

		public decimal MinAmazonSellPrice { get; set; }

		public decimal MaxAmazonSellPrice { get; set; }
		public decimal CurrentPrice { get; set; }
		public DateTime? DateUpdated { get; set; }
		public string Sku { get; set; }

	}
}
