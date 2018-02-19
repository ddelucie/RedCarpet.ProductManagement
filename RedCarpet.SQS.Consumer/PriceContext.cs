using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedCarpet.Data.Model;

namespace RedCarpet.SQS.Consumer
{
	public class PricingContext
	{
		public PricingResult PricingResult { get; set; }
		public Product Product { get; set; }

	}
}
