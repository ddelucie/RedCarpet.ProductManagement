namespace RedCarpet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Product : DbMigration
    {
        public override void Up()
        {
			// CREATING MANUALLY
            //CreateTable(
            //    "dbo.Products",
            //    c => new
            //        {
            //            ProductId = c.Int(nullable: false, identity: true),
            //            ItemNumber = c.String(),
            //            ASIN = c.String(),
            //            MinAmazonSellPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            MaxAmazonSellPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            CurrentPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
            //        })
            //    .PrimaryKey(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Products");
        }
    }
}
