namespace RedCarpet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SampleClasses",
                c => new
                    {
                        MyProperty = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.MyProperty);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SampleClasses");
        }
    }
}
