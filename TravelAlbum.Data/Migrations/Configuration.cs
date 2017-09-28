namespace TravelAlbum.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TravelAlbum.Models;

    public sealed class Configuration : DbMigrationsConfiguration<TravelCatalogEfDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;            
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TravelCatalogEfDbContext context)
        {
            Mountain rila = new Mountain()
            {
                Id = Guid.NewGuid()
            };

            context.Mountains.AddOrUpdate(rila);

            Travel malyovitsaTravel = new Travel
            {
                Id = Guid.NewGuid(),
                StartDate = new DateTime(2017, 08, 10),
                EndDate = new DateTime(2017, 08, 11),
                Mountain = rila
            };

            context.Travels.AddOrUpdate(malyovitsaTravel);

            context.TranslatedTravels.AddOrUpdate(new TravelTranslationalInfo
            {                
                Id = Guid.NewGuid(),
                Title = "Malyovitsa",
                Description = "Veliko mqsto",
                Language = Languages.English,
                Travel = malyovitsaTravel
            });

            context.TranslatedTravels.AddOrUpdate(new TravelTranslationalInfo
            {
                Id = Guid.NewGuid(),
                Title = "Мальовица",
                Description = "Велико място",
                Language = Languages.Bulgarian,
                Travel = malyovitsaTravel
            });
        }
    }
}
