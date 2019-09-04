namespace BS.TLFramework.Model
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class TLFrameworkDb : DbContext
    {
        //您的上下文已配置为从您的应用程序的配置文件(App.config 或 Web.config)
        //使用“TLFrameworkDb”连接字符串。默认情况下，此连接字符串针对您的 LocalDb 实例上的
        //“BS.TLFramework.Model.TLFrameworkDb”数据库。
        // 
        //如果您想要针对其他数据库和/或数据库提供程序，请在应用程序配置文件中修改“TLFrameworkDb”
        //连接字符串。


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

        //为您要在模型中包含的每种实体类型都添加 DbSet。有关配置和使用 Code First  模型
        //的详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=390109。

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingEntitySetNameConvention>();
        }
    }
}