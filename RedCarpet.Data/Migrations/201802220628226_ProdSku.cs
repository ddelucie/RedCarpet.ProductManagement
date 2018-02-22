namespace RedCarpet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProdSku : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Sku", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Sku");
        }
    }
}
