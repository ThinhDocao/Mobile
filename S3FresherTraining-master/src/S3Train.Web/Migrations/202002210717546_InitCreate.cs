namespace S3Train.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.About",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 250),
                        MetaTitle = c.String(maxLength: 250),
                        Descriptions = c.String(maxLength: 500),
                        Image = c.String(maxLength: 250),
                        Detail = c.String(storeType: "ntext"),
                        CreateDate = c.DateTime(),
                        CreateBy = c.String(maxLength: 50),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(maxLength: 50),
                        Metakeywords = c.String(maxLength: 250),
                        MetaDescriptions = c.String(maxLength: 250),
                        Status = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Quantity = c.Int(),
                        Price = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        OrderDetail_Id = c.Guid(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderDetails", t => t.OrderDetail_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.OrderDetail_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Content = c.String(storeType: "ntext"),
                        Status = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContentCategory",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 250),
                        MetaTitle = c.String(maxLength: 250),
                        ParentID = c.Long(),
                        DisplayOrder = c.Int(),
                        SeoTitle = c.String(maxLength: 250),
                        CreateDate = c.DateTime(),
                        CreateBy = c.String(maxLength: 50),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(maxLength: 50),
                        Metakeywords = c.String(maxLength: 250),
                        MetaDescriptions = c.String(maxLength: 250),
                        Status = c.Boolean(),
                        ShowOnHome = c.Boolean(),
                        ContentCategory_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContentCategory", t => t.ContentCategory_Id)
                .Index(t => t.ContentCategory_Id);
            
            CreateTable(
                "dbo.Content",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 250),
                        MetaTitle = c.String(maxLength: 250),
                        Descriptions = c.String(maxLength: 250),
                        Image = c.String(maxLength: 250),
                        Detail = c.String(),
                        Warranty = c.Int(),
                        CreateDate = c.DateTime(),
                        CreateBy = c.String(maxLength: 50),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(maxLength: 50),
                        Metakeywords = c.String(maxLength: 250),
                        MetaDescriptions = c.String(maxLength: 250),
                        Status = c.Boolean(),
                        TopHot = c.DateTime(),
                        ViewCount = c.Int(),
                        Tags = c.String(maxLength: 500),
                        ContentCategory_Id = c.Guid(),
                        ContentTag_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContentCategory", t => t.ContentCategory_Id)
                .ForeignKey("dbo.ContentTag", t => t.ContentTag_Id)
                .Index(t => t.ContentCategory_Id)
                .Index(t => t.ContentTag_Id);
            
            CreateTable(
                "dbo.ContentTag",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 50),
                        ContentTag_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContentTag", t => t.ContentTag_Id)
                .Index(t => t.ContentTag_Id);
            
            CreateTable(
                "dbo.Feedback",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 50),
                        Phone = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50),
                        Address = c.String(maxLength: 50),
                        Content = c.String(maxLength: 250),
                        CreatedDate = c.DateTime(),
                        Status = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Footer",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Content = c.String(storeType: "ntext"),
                        Status = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Text = c.String(maxLength: 50),
                        Link = c.String(maxLength: 250),
                        DisplayOrder = c.Int(),
                        Target = c.String(maxLength: 50),
                        Status = c.Boolean(),
                        MenuType_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MenuType", t => t.MenuType_Id)
                .Index(t => t.MenuType_Id);
            
            CreateTable(
                "dbo.MenuType",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Slide",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Image = c.String(maxLength: 250),
                        DisplayOrder = c.Int(),
                        Link = c.String(maxLength: 250),
                        Description = c.String(maxLength: 50),
                        CreateDate = c.DateTime(),
                        CreateBy = c.String(maxLength: 50),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(maxLength: 50),
                        Status = c.Boolean(),
                        Create = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SystemConfig",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 50),
                        Type = c.String(maxLength: 50),
                        Value = c.String(maxLength: 250),
                        Status = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Product", "OrderDetail_Id", c => c.Guid());
            CreateIndex("dbo.Product", "OrderDetail_Id");
            AddForeignKey("dbo.Product", "OrderDetail_Id", "dbo.OrderDetails", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Menu", "MenuType_Id", "dbo.MenuType");
            DropForeignKey("dbo.Tag", "ContentTag_Id", "dbo.ContentTag");
            DropForeignKey("dbo.Content", "ContentTag_Id", "dbo.ContentTag");
            DropForeignKey("dbo.Content", "ContentCategory_Id", "dbo.ContentCategory");
            DropForeignKey("dbo.ContentCategory", "ContentCategory_Id", "dbo.ContentCategory");
            DropForeignKey("dbo.Product", "OrderDetail_Id", "dbo.OrderDetails");
            DropForeignKey("dbo.Order", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Order", "OrderDetail_Id", "dbo.OrderDetails");
            DropIndex("dbo.Menu", new[] { "MenuType_Id" });
            DropIndex("dbo.Tag", new[] { "ContentTag_Id" });
            DropIndex("dbo.Content", new[] { "ContentTag_Id" });
            DropIndex("dbo.Content", new[] { "ContentCategory_Id" });
            DropIndex("dbo.ContentCategory", new[] { "ContentCategory_Id" });
            DropIndex("dbo.Order", new[] { "User_Id" });
            DropIndex("dbo.Order", new[] { "OrderDetail_Id" });
            DropIndex("dbo.Product", new[] { "OrderDetail_Id" });
            DropColumn("dbo.Product", "OrderDetail_Id");
            DropTable("dbo.SystemConfig");
            DropTable("dbo.Slide");
            DropTable("dbo.MenuType");
            DropTable("dbo.Menu");
            DropTable("dbo.Footer");
            DropTable("dbo.Feedback");
            DropTable("dbo.Tag");
            DropTable("dbo.ContentTag");
            DropTable("dbo.Content");
            DropTable("dbo.ContentCategory");
            DropTable("dbo.Contact");
            DropTable("dbo.Order");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.About");
        }
    }
}
