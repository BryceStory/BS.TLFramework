namespace BS.TLFramework.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "MerchantID", c => c.Long(nullable: false));
            CreateIndex("dbo.Users", "MerchantID");
            AddForeignKey("dbo.Users", "MerchantID", "dbo.Merchants", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "MerchantID", "dbo.Merchants");
            DropIndex("dbo.Users", new[] { "MerchantID" });
            DropColumn("dbo.Users", "MerchantID");
        }
    }
}
