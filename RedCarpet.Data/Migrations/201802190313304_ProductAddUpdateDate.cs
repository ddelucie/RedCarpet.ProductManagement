namespace RedCarpet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductAddUpdateDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "DateUpdated", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "DateUpdated");
        }
    }
}
