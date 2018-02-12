namespace RedCarpet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SampleClasses", "MyProperty2", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SampleClasses", "MyProperty2");
        }
    }
}
