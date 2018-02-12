namespace RedCarpet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PricingResultUpdate2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PricingResults", "PriceCategorySelected", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PricingResults", "PriceCategorySelected", c => c.Int(nullable: false));
        }
    }
}
