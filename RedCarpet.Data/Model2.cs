namespace RedCarpet.Data
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class Model2 : DbContext
	{
		public Model2()
			: base("name=RCDBContext")
		{
		}


		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
		}
	}
}
