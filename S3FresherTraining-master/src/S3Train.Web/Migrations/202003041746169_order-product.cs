namespace S3Train.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderproduct : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Order", "OrderDetail_Id", "dbo.OrderDetails");
            DropForeignKey("dbo.Product", "OrderDetail_Id", "dbo.OrderDetails");
            DropIndex("dbo.Product", new[] { "OrderDetail_Id" });
            DropIndex("dbo.Order", new[] { "OrderDetail_Id" });
            RenameColumn(table: "dbo.Order", name: "User_Id", newName: "ApplicationUser_Id");
            RenameIndex(table: "dbo.Order", name: "IX_User_Id", newName: "IX_ApplicationUser_Id");
            AddColumn("dbo.OrderDetails", "Order_Id", c => c.Guid());
            AddColumn("dbo.OrderDetails", "Product_Id", c => c.Guid());
            AddColumn("dbo.OrderDetails", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.OrderDetails", "Order_Id");
            CreateIndex("dbo.OrderDetails", "Product_Id");
            CreateIndex("dbo.OrderDetails", "User_Id");
            AddForeignKey("dbo.OrderDetails", "Order_Id", "dbo.Order", "Id");
            AddForeignKey("dbo.OrderDetails", "Product_Id", "dbo.Product", "Id");
            AddForeignKey("dbo.OrderDetails", "User_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Product", "OrderDetail_Id");
            DropColumn("dbo.Order", "OrderDetail_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "OrderDetail_Id", c => c.Guid());
            AddColumn("dbo.Product", "OrderDetail_Id", c => c.Guid());
            DropForeignKey("dbo.OrderDetails", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderDetails", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.OrderDetails", "Order_Id", "dbo.Order");
            DropIndex("dbo.OrderDetails", new[] { "User_Id" });
            DropIndex("dbo.OrderDetails", new[] { "Product_Id" });
            DropIndex("dbo.OrderDetails", new[] { "Order_Id" });
            DropColumn("dbo.OrderDetails", "User_Id");
            DropColumn("dbo.OrderDetails", "Product_Id");
            DropColumn("dbo.OrderDetails", "Order_Id");
            RenameIndex(table: "dbo.Order", name: "IX_ApplicationUser_Id", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Order", name: "ApplicationUser_Id", newName: "User_Id");
            CreateIndex("dbo.Order", "OrderDetail_Id");
            CreateIndex("dbo.Product", "OrderDetail_Id");
            AddForeignKey("dbo.Product", "OrderDetail_Id", "dbo.OrderDetails", "Id");
            AddForeignKey("dbo.Order", "OrderDetail_Id", "dbo.OrderDetails", "Id");
        }
    }
}
