namespace S3Train.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitCreate4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductCategory",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 250),
                        MetaTitle = c.String(maxLength: 250),
                        ParentID = c.Long(),
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
                "dbo.Product",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductCategoryID = c.Guid(nullable: false),
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
                .Index(t => t.ProductCategoryID);
            
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
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
            DropForeignKey("dbo.Product", "ProductCategoryID", "dbo.ProductCategory");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Product", new[] { "ProductCategoryID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ProductAdvertisement");
            DropTable("dbo.Product");
            DropTable("dbo.ProductCategory");
        }
    }
}
