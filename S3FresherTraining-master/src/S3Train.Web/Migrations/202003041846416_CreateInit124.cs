namespace S3Train.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateInit124 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "Order_Id", "dbo.Order");
            DropForeignKey("dbo.OrderDetails", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.Order", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderDetails", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.OrderDetails", new[] { "Order_Id" });
            DropIndex("dbo.OrderDetails", new[] { "Product_Id" });
            DropIndex("dbo.OrderDetails", new[] { "User_Id" });
            DropIndex("dbo.Order", new[] { "ApplicationUser_Id" });
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Order");
        }
        
        public override void Down()
        {
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
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Order", "ApplicationUser_Id");
            CreateIndex("dbo.OrderDetails", "User_Id");
            CreateIndex("dbo.OrderDetails", "Product_Id");
            CreateIndex("dbo.OrderDetails", "Order_Id");
            AddForeignKey("dbo.OrderDetails", "User_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Order", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.OrderDetails", "Product_Id", "dbo.Product", "Id");
            AddForeignKey("dbo.OrderDetails", "Order_Id", "dbo.Order", "Id");
        }
    }
}
