using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedCarpet.Data.Model;

namespace RedCarpet.Data.Tests
{
	[TestClass]
	public class DataRepositoryTests
	{
		[TestMethod]
		public void FindProductTest()
		{
			IDataRepository repo = new DataRepository();

			var product = repo.Find<Product>(1).Result;

			Assert.IsNotNull(product);
			Console.WriteLine(product.ASIN);
		}
		[TestMethod]
		public void FindPricingResultTest()
		{
			IDataRepository repo = new DataRepository();

			var product = repo.Find<PricingResult>(10).Result;

			Assert.IsNotNull(product);
			Console.WriteLine(product.ASIN);
		}


		[TestMethod]
		public void UpdateProductTest()
		{
			IDataRepository repo = new DataRepository();

			var product = repo.Find<Product>(1).Result;

			Assert.IsNotNull(product);
			Console.WriteLine(product.ASIN);

			var newPrice = product.CurrentPrice + 1m;
			product.CurrentPrice = newPrice;
			repo.Update(product);

			product = repo.Find<Product>(1).Result;
			Assert.AreEqual(newPrice, product.CurrentPrice);

		}
	}
}
