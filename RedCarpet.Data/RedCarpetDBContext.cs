using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedCarpet.Data.Model;

namespace RedCarpet.Data
{
	public class RedCarpetDBContext : DbContext
	{
		public virtual DbSet<SampleClass> SampleClasses { get; set; }
		public virtual DbSet<PricingResult> PricingResults { get; set; }

	}

}
