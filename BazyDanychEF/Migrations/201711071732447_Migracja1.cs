namespace BazyDanychEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migracja1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(maxLength: 128),
                        ProductID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CompanyName)
                .Index(t => t.CompanyName)
                .Index(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "CompanyName", "dbo.Customers");
            DropForeignKey("dbo.Orders", "ProductID", "dbo.Products");
            DropIndex("dbo.Orders", new[] { "ProductID" });
            DropIndex("dbo.Orders", new[] { "CompanyName" });
            DropTable("dbo.Orders");
        }
    }
}
