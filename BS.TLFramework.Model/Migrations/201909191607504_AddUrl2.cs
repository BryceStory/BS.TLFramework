namespace BS.TLFramework.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddUrl2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                {
                    ID = c.Long(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 16),
                    ParentID = c.Long(),
                    Status = c.Int(nullable: false, defaultValue: 0),
                    CreateDatetime = c.DateTime(nullable: false, defaultValueSql: "getdate()"),
                    ModifyDatetime = c.DateTime(),
                    RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Departments", t => t.ParentID)
                .Index(t => t.Name, unique: true)
                .Index(t => t.ParentID);

            CreateTable(
                "dbo.Roles",
                c => new
                {
                    ID = c.Long(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 16),
                    Menus = c.String(),
                    MenuActions = c.String(),
                    Status = c.Int(nullable: false, defaultValue: 0),
                    CreateDatetime = c.DateTime(nullable: false, defaultValueSql: "getdate()"),
                    ModifyDatetime = c.DateTime(),
                    RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Name, unique: true);

            CreateTable(
                "dbo.Employees",
                c => new
                {
                    ID = c.Long(nullable: false, identity: true),
                    MerchantID = c.Long(nullable: false),
                    Code = c.String(nullable: false, maxLength: 16),
                    Name = c.String(nullable: false, maxLength: 64),
                    Email = c.String(maxLength: 64),
                    Mobile = c.String(maxLength: 11),
                    Password = c.String(nullable: false, maxLength: 32),
                    IsAdmin = c.Int(nullable: false),
                    IsMore = c.Int(nullable: false),
                    AuthDepartmentIDs = c.String(),
                    EmployeeRoleID = c.Long(),
                    EmployeeDepartmentID = c.Long(),
                    Status = c.Int(nullable: false, defaultValue: 0),
                    CreateDatetime = c.DateTime(nullable: false, defaultValueSql: "getdate()"),
                    ModifyDatetime = c.DateTime(),
                    RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EmployeeDepartments", t => t.EmployeeDepartmentID)
                .ForeignKey("dbo.EmployeeRoles", t => t.EmployeeRoleID)
                .ForeignKey("dbo.Merchants", t => t.MerchantID, cascadeDelete: true)
                .Index(t => t.MerchantID)
                .Index(t => t.EmployeeRoleID)
                .Index(t => t.EmployeeDepartmentID);

            CreateTable(
                "dbo.EmployeeDepartments",
                c => new
                {
                    ID = c.Long(nullable: false, identity: true),
                    MerchantID = c.Long(nullable: false),
                    Name = c.String(nullable: false, maxLength: 16),
                    ParentID = c.Long(),
                    Status = c.Int(nullable: false, defaultValue: 0),
                    CreateDatetime = c.DateTime(nullable: false, defaultValueSql: "getdate()"),
                    ModifyDatetime = c.DateTime(),
                    RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EmployeeDepartments", t => t.ParentID)
                .ForeignKey("dbo.Merchants", t => t.MerchantID, cascadeDelete: true)
                .Index(t => t.MerchantID)
                .Index(t => t.Name, unique: true)
                .Index(t => t.ParentID);

            CreateTable(
                "dbo.Merchants",
                c => new
                {
                    ID = c.Long(nullable: false, identity: true),
                    Code = c.String(nullable: false, maxLength: 16),
                    Name = c.String(nullable: false, maxLength: 64),
                    Contact = c.String(maxLength: 16),
                    Mobile = c.String(maxLength: 16),
                    Status = c.Int(nullable: false, defaultValue: 0),
                    CreateDatetime = c.DateTime(nullable: false, defaultValueSql: "getdate()"),
                    ModifyDatetime = c.DateTime(),
                    RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.EmployeeRoles",
                c => new
                {
                    ID = c.Long(nullable: false, identity: true),
                    MerchantID = c.Long(nullable: false),
                    Name = c.String(nullable: false, maxLength: 16),
                    Menus = c.String(),
                    MenuActions = c.String(),
                    Status = c.Int(nullable: false, defaultValue: 0),
                    CreateDatetime = c.DateTime(nullable: false, defaultValueSql: "getdate()"),
                    ModifyDatetime = c.DateTime(),
                    RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Merchants", t => t.MerchantID, cascadeDelete: true)
                .Index(t => t.MerchantID)
                .Index(t => t.Name, unique: true);

            CreateTable(
                "dbo.EmployeeMenus",
                c => new
                {
                    Code = c.String(nullable: false, maxLength: 16),
                    Name = c.String(nullable: false, maxLength: 16),
                    Url = c.String(maxLength: 64),
                    ParentCode = c.String(maxLength: 16),
                    Sort = c.Int(nullable: false),
                    Controller = c.String(maxLength: 64),
                    Action = c.String(maxLength: 64),
                    ID = c.Long(nullable: false, identity: true),
                    Status = c.Int(nullable: false, defaultValue: 0),
                    CreateDatetime = c.DateTime(nullable: false, defaultValueSql: "getdate()"),
                    ModifyDatetime = c.DateTime(),
                    RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                })
                .PrimaryKey(t => t.Code)
                .ForeignKey("dbo.EmployeeMenus", t => t.ParentCode)
                .Index(t => t.Code, unique: true)
                .Index(t => t.ParentCode);

            CreateTable(
                "dbo.EmployeeMenuActions",
                c => new
                {
                    ID = c.Long(nullable: false, identity: true),
                    MenuCode = c.String(nullable: false, maxLength: 16),
                    Code = c.String(nullable: false, maxLength: 32),
                    Name = c.String(nullable: false, maxLength: 16),
                    Sort = c.Int(nullable: false),
                    Status = c.Int(nullable: false, defaultValue: 0),
                    CreateDatetime = c.DateTime(nullable: false, defaultValueSql: "getdate()"),
                    ModifyDatetime = c.DateTime(),
                    RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EmployeeMenus", t => t.MenuCode, cascadeDelete: true)
                .Index(t => new { t.MenuCode, t.Code }, unique: true, name: "EmployeeMenuAction_IX");

            CreateTable(
                "dbo.Menus",
                c => new
                {
                    Code = c.String(nullable: false, maxLength: 16),
                    Name = c.String(nullable: false, maxLength: 16),
                    Url = c.String(maxLength: 64),
                    ParentCode = c.String(maxLength: 16),
                    Sort = c.Int(nullable: false),
                    Controller = c.String(maxLength: 64),
                    Action = c.String(maxLength: 64),
                    ID = c.Long(nullable: false, identity: true),
                    Status = c.Int(nullable: false, defaultValue: 0),
                    CreateDatetime = c.DateTime(nullable: false, defaultValueSql: "getdate()"),
                    ModifyDatetime = c.DateTime(),
                    RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                })
                .PrimaryKey(t => t.Code)
                .ForeignKey("dbo.Menus", t => t.ParentCode)
                .Index(t => t.Code, unique: true)
                .Index(t => t.ParentCode);

            CreateTable(
                "dbo.MenuActions",
                c => new
                {
                    ID = c.Long(nullable: false, identity: true),
                    MenuCode = c.String(nullable: false, maxLength: 16),
                    Code = c.String(nullable: false, maxLength: 32),
                    Name = c.String(nullable: false, maxLength: 16),
                    Sort = c.Int(nullable: false),
                    Status = c.Int(nullable: false, defaultValue: 0),
                    CreateDatetime = c.DateTime(nullable: false, defaultValueSql: "getdate()"),
                    ModifyDatetime = c.DateTime(),
                    RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Menus", t => t.MenuCode, cascadeDelete: true)
                .Index(t => new { t.MenuCode, t.Code }, unique: true, name: "MenuAction_IX");

            CreateIndex("dbo.Users", "RoleID");
            CreateIndex("dbo.Users", "DepartmentID");
            AddForeignKey("dbo.Users", "RoleID", "dbo.Roles", "ID");
            AddForeignKey("dbo.Users", "DepartmentID", "dbo.Departments", "ID");
        }

        public override void Down()
        {
            DropForeignKey("dbo.Menus", "ParentCode", "dbo.Menus");
            DropForeignKey("dbo.MenuActions", "MenuCode", "dbo.Menus");
            DropForeignKey("dbo.EmployeeMenus", "ParentCode", "dbo.EmployeeMenus");
            DropForeignKey("dbo.EmployeeMenuActions", "MenuCode", "dbo.EmployeeMenus");
            DropForeignKey("dbo.Employees", "MerchantID", "dbo.Merchants");
            DropForeignKey("dbo.Employees", "EmployeeRoleID", "dbo.EmployeeRoles");
            DropForeignKey("dbo.EmployeeRoles", "MerchantID", "dbo.Merchants");
            DropForeignKey("dbo.EmployeeDepartments", "MerchantID", "dbo.Merchants");
            DropForeignKey("dbo.Employees", "EmployeeDepartmentID", "dbo.EmployeeDepartments");
            DropForeignKey("dbo.EmployeeDepartments", "ParentID", "dbo.EmployeeDepartments");
            DropForeignKey("dbo.Users", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.Users", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.Departments", "ParentID", "dbo.Departments");
            DropIndex("dbo.MenuActions", "MenuAction_IX");
            DropIndex("dbo.Menus", new[] { "ParentCode" });
            DropIndex("dbo.Menus", new[] { "Code" });
            DropIndex("dbo.EmployeeMenuActions", "EmployeeMenuAction_IX");
            DropIndex("dbo.EmployeeMenus", new[] { "ParentCode" });
            DropIndex("dbo.EmployeeMenus", new[] { "Code" });
            DropIndex("dbo.EmployeeRoles", new[] { "Name" });
            DropIndex("dbo.EmployeeRoles", new[] { "MerchantID" });
            DropIndex("dbo.EmployeeDepartments", new[] { "ParentID" });
            DropIndex("dbo.EmployeeDepartments", new[] { "Name" });
            DropIndex("dbo.EmployeeDepartments", new[] { "MerchantID" });
            DropIndex("dbo.Employees", new[] { "EmployeeDepartmentID" });
            DropIndex("dbo.Employees", new[] { "EmployeeRoleID" });
            DropIndex("dbo.Employees", new[] { "MerchantID" });
            DropIndex("dbo.Roles", new[] { "Name" });
            DropIndex("dbo.Users", new[] { "DepartmentID" });
            DropIndex("dbo.Users", new[] { "RoleID" });
            DropIndex("dbo.Departments", new[] { "ParentID" });
            DropIndex("dbo.Departments", new[] { "Name" });
            DropTable("dbo.MenuActions");
            DropTable("dbo.Menus");
            DropTable("dbo.EmployeeMenuActions");
            DropTable("dbo.EmployeeMenus");
            DropTable("dbo.EmployeeRoles");
            DropTable("dbo.Merchants");
            DropTable("dbo.EmployeeDepartments");
            DropTable("dbo.Employees");
            DropTable("dbo.Roles");
            DropTable("dbo.Departments");
        }
    }
}
