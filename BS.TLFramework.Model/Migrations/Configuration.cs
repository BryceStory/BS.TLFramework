namespace BS.TLFramework.Model.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BS.TLFramework.Model.TLFrameworkDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;   //Ĭ��Ϊfalse����Ҫ����Ϊtrue
        }

        protected override void Seed(BS.TLFramework.Model.TLFrameworkDb context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
