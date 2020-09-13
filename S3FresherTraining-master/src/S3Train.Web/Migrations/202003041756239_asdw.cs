namespace S3Train.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asdw : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "Product_Id", "dbo.Product");
            DropIndex("dbo.OrderDetails", new[] { "Product_Id" });
            DropColumn("dbo.OrderDetails", "Product_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "Product_Id", c => c.Guid());
            CreateIndex("dbo.OrderDetails", "Product_Id");
            AddForeignKey("dbo.OrderDetails", "Product_Id", "dbo.Product", "Id");
        }
    }
}
