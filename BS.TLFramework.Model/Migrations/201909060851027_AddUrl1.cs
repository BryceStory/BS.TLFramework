namespace BS.TLFramework.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "CreateDatetime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "CreateDatetime", c => c.DateTime(nullable: false));
        }
    }
}
