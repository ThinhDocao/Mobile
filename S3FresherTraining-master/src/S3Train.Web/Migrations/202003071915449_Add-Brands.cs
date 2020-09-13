namespace S3Train.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBrands : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Brands", newName: "Brand");
            DropForeignKey("dbo.Product", "Brand_Id", "dbo.Brands");
            DropIndex("dbo.Product", new[] { "Brand_Id" });
            RenameColumn(table: "dbo.Product", name: "Brand_Id", newName: "BrandID");
            AlterColumn("dbo.Product", "BrandID", c => c.Guid(nullable: false));
            AlterColumn("dbo.Brand", "Name", c => c.String(maxLength: 250));
            AlterColumn("dbo.Brand", "CreateBy", c => c.String(maxLength: 50));
            AlterColumn("dbo.Brand", "ModifyBy", c => c.String(maxLength: 50));
            CreateIndex("dbo.Product", "BrandID");
            AddForeignKey("dbo.Product", "BrandID", "dbo.Brand", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "BrandID", "dbo.Brand");
            DropIndex("dbo.Product", new[] { "BrandID" });
            AlterColumn("dbo.Brand", "ModifyBy", c => c.String());
            AlterColumn("dbo.Brand", "CreateBy", c => c.String());
            AlterColumn("dbo.Brand", "Name", c => c.String());
            AlterColumn("dbo.Product", "BrandID", c => c.Guid());
            RenameColumn(table: "dbo.Product", name: "BrandID", newName: "Brand_Id");
            CreateIndex("dbo.Product", "Brand_Id");
            AddForeignKey("dbo.Product", "Brand_Id", "dbo.Brands", "Id");
            RenameTable(name: "dbo.Brand", newName: "Brands");
        }
    }
}
