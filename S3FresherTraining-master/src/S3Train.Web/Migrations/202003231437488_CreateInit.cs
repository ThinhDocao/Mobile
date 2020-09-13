namespace S3Train.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateInit : DbMigration
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
                "dbo.Answer",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        QuestionID = c.Guid(nullable: false),
                        Content = c.String(maxLength: 500),
                        CreateDate = c.DateTime(),
                        CreateBy = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Question", t => t.QuestionID, cascadeDelete: true)
                .Index(t => t.QuestionID);
            
            CreateTable(
                "dbo.Question",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(maxLength: 250),
                        Content = c.String(maxLength: 500),
                        CreateDate = c.DateTime(),
                        CreateBy = c.String(maxLength: 50),
                        Status = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Brand",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 250),
                        Logo = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        CreateBy = c.String(maxLength: 50),
                        ModifyDate = c.DateTime(nullable: false),
                        ModifyBy = c.String(maxLength: 50),
                        Status = c.Boolean(),
                        MetaTitle = c.String(maxLength: 250),
                        DisplayOrder = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductCategoryID = c.Guid(nullable: false),
                        BrandID = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 250),
                        Code = c.String(maxLength: 20),
                        MetaTitle = c.String(maxLength: 250),
                        Descriptions = c.String(maxLength: 500),
                        Image = c.String(maxLength: 250),
                        MoreImage = c.String(storeType: "xml"),
                        Price = c.Decimal(precision: 18, scale: 2),
                        PromotionPrice = c.Decimal(precision: 18, scale: 2),
                        IncludeVAT = c.Boolean(),
                        Quantity = c.Int(),
                        Detail = c.String(storeType: "ntext"),
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductCategory", t => t.ProductCategoryID, cascadeDelete: true)
                .ForeignKey("dbo.Brand", t => t.BrandID, cascadeDelete: true)
                .Index(t => t.ProductCategoryID)
                .Index(t => t.BrandID);
            
            CreateTable(
                "dbo.OrderDetail",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Quantity = c.Int(),
                        Price = c.Decimal(precision: 18, scale: 2),
                        OrderID = c.Guid(nullable: false),
                        ProductID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreateDate = c.DateTime(),
                        CreateBy = c.String(maxLength: 50),
                        ShipName = c.String(maxLength: 50),
                        ShipMobile = c.String(maxLength: 50),
                        ShipAddress = c.String(maxLength: 50),
                        ShipEmail = c.String(maxLength: 50),
                        Status = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductCategory",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 250),
                        MetaTitle = c.String(maxLength: 250),
                        ParentID = c.Long(),
                        Image = c.String(maxLength: 250),
                        DisplayOrder = c.Int(),
                        SeoTitle = c.String(maxLength: 250),
                        CreateDate = c.DateTime(),
                        CreateBy = c.String(maxLength: 250),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(maxLength: 50),
                        Metakeywords = c.String(maxLength: 250),
                        MetaDescriptions = c.String(maxLength: 250),
                        Status = c.Boolean(),
                        ShowOnHome = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductSpecSpecValue",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductID = c.Guid(nullable: false),
                        SpecID = c.Guid(nullable: false),
                        SpecValueID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Spec", t => t.SpecID, cascadeDelete: true)
                .ForeignKey("dbo.SpecValue", t => t.SpecValueID, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.SpecID)
                .Index(t => t.SpecValueID);
            
            CreateTable(
                "dbo.Spec",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SpecValue",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        NameSpecValue = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
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
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Content",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ContentCategoryID = c.Guid(nullable: false),
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContentCategory", t => t.ContentCategoryID, cascadeDelete: true)
                .Index(t => t.ContentCategoryID);
            
            CreateTable(
                "dbo.ContentTag",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ContentID = c.Guid(nullable: false),
                        TagID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tag", t => t.TagID, cascadeDelete: true)
                .ForeignKey("dbo.Content", t => t.ContentID, cascadeDelete: true)
                .Index(t => t.ContentID)
                .Index(t => t.TagID);
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        MenuTypeID = c.Guid(nullable: false),
                        Text = c.String(maxLength: 50),
                        Image = c.String(maxLength: 50),
                        Link = c.String(maxLength: 250),
                        DisplayOrder = c.Int(),
                        Target = c.String(maxLength: 50),
                        Status = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MenuType", t => t.MenuTypeID, cascadeDelete: true)
                .Index(t => t.MenuTypeID);
            
            CreateTable(
                "dbo.MenuType",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderDetailTemp",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserID = c.String(maxLength: 255),
                        CartContent = c.String(storeType: "ntext"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductAdvertisement",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(maxLength: 100),
                        Description = c.String(maxLength: 500),
                        EventUrl = c.String(nullable: false, maxLength: 50),
                        EventUrlCaption = c.String(maxLength: 10),
                        ImagePath = c.String(nullable: false, maxLength: 200),
                        Status = c.Boolean(nullable: false),
                        AdType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Image = c.String(),
                        Name = c.String(),
                        Address = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        CreateBy = c.String(),
                        ModifyDate = c.DateTime(nullable: false),
                        ModifyBy = c.String(),
                        status = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Menu", "MenuTypeID", "dbo.MenuType");
            DropForeignKey("dbo.ContentTag", "ContentID", "dbo.Content");
            DropForeignKey("dbo.ContentTag", "TagID", "dbo.Tag");
            DropForeignKey("dbo.Content", "ContentCategoryID", "dbo.ContentCategory");
            DropForeignKey("dbo.Product", "BrandID", "dbo.Brand");
            DropForeignKey("dbo.ProductSpecSpecValue", "ProductID", "dbo.Product");
            DropForeignKey("dbo.ProductSpecSpecValue", "SpecValueID", "dbo.SpecValue");
            DropForeignKey("dbo.ProductSpecSpecValue", "SpecID", "dbo.Spec");
            DropForeignKey("dbo.Product", "ProductCategoryID", "dbo.ProductCategory");
            DropForeignKey("dbo.OrderDetail", "ProductID", "dbo.Product");
            DropForeignKey("dbo.OrderDetail", "OrderID", "dbo.Order");
            DropForeignKey("dbo.Answer", "QuestionID", "dbo.Question");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Menu", new[] { "MenuTypeID" });
            DropIndex("dbo.ContentTag", new[] { "TagID" });
            DropIndex("dbo.ContentTag", new[] { "ContentID" });
            DropIndex("dbo.Content", new[] { "ContentCategoryID" });
            DropIndex("dbo.ProductSpecSpecValue", new[] { "SpecValueID" });
            DropIndex("dbo.ProductSpecSpecValue", new[] { "SpecID" });
            DropIndex("dbo.ProductSpecSpecValue", new[] { "ProductID" });
            DropIndex("dbo.OrderDetail", new[] { "ProductID" });
            DropIndex("dbo.OrderDetail", new[] { "OrderID" });
            DropIndex("dbo.Product", new[] { "BrandID" });
            DropIndex("dbo.Product", new[] { "ProductCategoryID" });
            DropIndex("dbo.Answer", new[] { "QuestionID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.SystemConfig");
            DropTable("dbo.Slide");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ProductAdvertisement");
            DropTable("dbo.OrderDetailTemp");
            DropTable("dbo.MenuType");
            DropTable("dbo.Menu");
            DropTable("dbo.Footer");
            DropTable("dbo.Feedback");
            DropTable("dbo.Tag");
            DropTable("dbo.ContentTag");
            DropTable("dbo.Content");
            DropTable("dbo.ContentCategory");
            DropTable("dbo.Contact");
            DropTable("dbo.SpecValue");
            DropTable("dbo.Spec");
            DropTable("dbo.ProductSpecSpecValue");
            DropTable("dbo.ProductCategory");
            DropTable("dbo.Order");
            DropTable("dbo.OrderDetail");
            DropTable("dbo.Product");
            DropTable("dbo.Brand");
            DropTable("dbo.Question");
            DropTable("dbo.Answer");
            DropTable("dbo.About");
        }
    }
}
