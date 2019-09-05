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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingEntitySetNameConvention>();
        }
    }
}