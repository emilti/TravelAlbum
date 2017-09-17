namespace TravelCatalog.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TravelCatalog.Models;

    public sealed class Configuration : DbMigrationsConfiguration<TravelCatalogDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(TravelCatalogDbContext context)
        {
            context.Travels.AddOrUpdate(x => x.Title, new Travel
            {
                Title = "Malyovitsa",
                Description = "Veliko mqsto"
            });
        }
    }
}
