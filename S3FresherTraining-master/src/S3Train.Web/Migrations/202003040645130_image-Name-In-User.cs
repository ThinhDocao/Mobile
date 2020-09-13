namespace S3Train.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imageNameInUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Image", c => c.String());
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Name");
            DropColumn("dbo.AspNetUsers", "Image");
        }
    }
}
