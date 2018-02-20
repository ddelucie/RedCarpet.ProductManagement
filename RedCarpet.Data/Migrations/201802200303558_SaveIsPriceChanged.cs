namespace RedCarpet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SaveIsPriceChanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PricingResults", "IsPriceChanged", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PricingResults", "IsPriceChanged");
        }
    }
}
