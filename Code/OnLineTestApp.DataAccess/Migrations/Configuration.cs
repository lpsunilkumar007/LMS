namespace OnlineTestApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OnlineTestApp.DataAccess.DataLayer.OnlineTestAppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
           // ContextKey = "OnlineTestApp.DataAccess.DataLayer.OnlineTestAppContext";
        }

        protected override void Seed(OnlineTestApp.DataAccess.DataLayer.OnlineTestAppContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
