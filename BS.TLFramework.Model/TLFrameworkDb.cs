namespace BS.TLFramework.Model
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class TLFrameworkDb : DbContext
    {
        ///   创建code first时先要：Enable-Migrations ,则 会自动生成迁移文件夹Migrations

        /// 数据库升级
        ///1、输入 Add-Migration AddUrl
        ///2、输入 update-database
        /// 
        /// 
        /// 降级:   Update-Database –TargetMigration: 类名 
        /// 
        /// 生成SQL语句：    update-database -script 
        /// 


        public TLFrameworkDb()
            : base("name=TLFramework")
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeDepartment> EmployeeDepartment { get; set; }
        public DbSet<EmployeeMenu> EmployeeMenu { get; set; }
        public DbSet<EmployeeMenuAction> EmployeeMenuAction { get; set; }
        public DbSet<EmployeeRole> EmployeeRole { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<MenuAction> MenuAction { get; set; }
        public DbSet<Merchant> Merchant { get; set; }
        public DbSet<Role> Role { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingEntitySetNameConvention>();

            ForeignKey(modelBuilder);
        }


        /// <summary>
        /// 设置关联
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void ForeignKey(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasMany(e => e.ChildDepartment).WithOptional(e => e.ParentDepartment).HasForeignKey(e => e.ParentID);
            modelBuilder.Entity<Department>().HasMany(e => e.UserList).WithOptional(e => e.Department).HasForeignKey(e => e.DepartmentID);

            modelBuilder.Entity<EmployeeDepartment>().HasMany(e => e.ChildDepartment).WithOptional(e => e.ParentDepartment).HasForeignKey(e => e.ParentID);
            modelBuilder.Entity<EmployeeDepartment>().HasMany(e => e.EmployeeList).WithOptional(e => e.EmployeeDepartment).HasForeignKey(e => e.EmployeeDepartmentID);



        }
    }
}