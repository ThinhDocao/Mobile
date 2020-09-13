namespace S3Train.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitCreate2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Slide", "Create");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Slide", "Create", c => c.String(maxLength: 10));
        }
    }
}
