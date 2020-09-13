namespace S3Train.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateInit125 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Quantity = c.Int(),
                        Price = c.Decimal(precision: 18, scale: 2),
                        Order_Id = c.Guid(),
                        Product_Id = c.Guid(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.Order_Id)
                .ForeignKey("dbo.Product", t => t.Product_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Order_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreateDate = c.DateTime(),
                        ShipName = c.String(maxLength: 50),
                        ShipMobile = c.String(maxLength: 50),
                        ShipAddress = c.String(maxLength: 50),
                        ShipEmail = c.String(maxLength: 50),
                        Status = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderDetails", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.OrderDetails", "Order_Id", "dbo.Order");
            DropIndex("dbo.OrderDetails", new[] { "User_Id" });
            DropIndex("dbo.OrderDetails", new[] { "Product_Id" });
            DropIndex("dbo.OrderDetails", new[] { "Order_Id" });
            DropTable("dbo.Order");
            DropTable("dbo.OrderDetails");
        }
    }
}
