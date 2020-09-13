namespace S3Train.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnAddressInUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            DropColumn("dbo.AspNetUsers", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
            DropColumn("dbo.AspNetUsers", "Address");
        }
    }
}
