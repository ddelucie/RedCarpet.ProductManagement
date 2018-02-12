namespace RedCarpet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PricingResultUpdate1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PricingResults",
                c => new
                    {
                        PricingResultHistoryId = c.Long(nullable: false, identity: true),
                        NewPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OriginalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LandedPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ListingPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MaxPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MinPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PriceCategorySelected = c.Int(nullable: false),
                        TimeOfOfferChange = c.DateTime(nullable: false),
                        ASIN = c.String(),
                        DateEntry = c.DateTime(nullable: false),
                        PriceUpdateSucceeded = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PricingResultHistoryId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PricingResults");
        }
    }
}
