namespace BS.TLFramework.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 16),
                        Name = c.String(nullable: false, maxLength: 32),
                        Email = c.String(maxLength: 64),
                        Mobile = c.String(maxLength: 11),
                        Password = c.String(nullable: false, maxLength: 32),
                        IsMore = c.Int(nullable: false),
                        LastLoginDate = c.DateTime(),
                        RoleID = c.Long(),
                        DepartmentID = c.Long(),
                        AuthDepartmentIDs = c.String(),
                        Status = c.Int(nullable: false),
                        CreateDatetime = c.DateTime(nullable: false),
                        ModifyDatetime = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Code, unique: true)
                .Index(t => t.Name, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "Name" });
            DropIndex("dbo.Users", new[] { "Code" });
            DropTable("dbo.Users");
        }
    }
}
