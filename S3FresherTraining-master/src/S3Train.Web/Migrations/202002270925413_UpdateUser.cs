namespace S3Train.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "CreateBy", c => c.String());
            AddColumn("dbo.AspNetUsers", "ModifyDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "ModifyBy", c => c.String());
            AddColumn("dbo.AspNetUsers", "status", c => c.Boolean(nullable: true,defaultValue :true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "status");
            DropColumn("dbo.AspNetUsers", "ModifyBy");
            DropColumn("dbo.AspNetUsers", "ModifyDate");
            DropColumn("dbo.AspNetUsers", "CreateBy");
            DropColumn("dbo.AspNetUsers", "CreateDate");
        }
    }
}
