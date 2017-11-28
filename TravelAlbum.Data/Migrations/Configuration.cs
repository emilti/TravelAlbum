namespace TravelAlbum.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using TravelAlbum.Models;

    public sealed class Configuration : DbMigrationsConfiguration<TravelAlbum.Data.TravelAlbumEfDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TravelAlbum.Data.TravelAlbumEfDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "User" });
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            var adminUser = userManager.Users.FirstOrDefault(x => x.Email == "admin@admin.com");
            if (adminUser == null)
            {
                var admin = new ApplicationUser
                {
                    NickName = "admin",
                    Email = "admin@admin.com",
                    UserName = "admin@admin.com",
                    EmailConfirmed = true,
                    Avatar = "http://www.premiumdxb.com/assets/img/avatar/default-avatar.jpg"                   
                };

                userManager.Create(admin, "123456");
                userManager.AddToRole(admin.Id, "Admin");
            }

            if (userManager.Users.FirstOrDefault(x => x.Email == "user1@mail.com") == null)
            {
                var user = new ApplicationUser
                {
                    NickName = "user1",
                    Email = "user1@mail.com",
                    UserName = "user1@mail.com",
                    Avatar = "http://www.premiumdxb.com/assets/img/avatar/default-avatar.jpg"                    
                };

                userManager.Create(user, "123456");
                userManager.AddToRole(user.Id, "User");
            }

            if (userManager.Users.FirstOrDefault(x => x.Email == "user2@mail.com") == null)
            {
                var user = new ApplicationUser
                {
                    NickName = "user2",
                    Email = "user2@mail.com",
                    UserName = "user2@mail.com",
                    Avatar = "http://www.premiumdxb.com/assets/img/avatar/default-avatar.jpg"                    
                };

                userManager.Create(user, "123456");
                userManager.AddToRole(user.Id, "User");
            }

            if (userManager.Users.FirstOrDefault(x => x.Email == "user3@mail.com") == null)
            {
                var user = new ApplicationUser
                {
                    NickName = "user3",
                    Email = "user3@mail.com",
                    UserName = "user3@mail.com",
                    Avatar = "http://www.premiumdxb.com/assets/img/avatar/default-avatar.jpg"                  
                };

                userManager.Create(user, "123456");
                userManager.AddToRoles(user.Id, "User", "Admin");
            }

            if (userManager.Users.FirstOrDefault(x => x.Email == "user4@mail.com") == null)
            {
                var user = new ApplicationUser
                {
                    NickName = "user4",
                    Email = "user4@mail.com",
                    UserName = "user4@mail.com",
                    Avatar = "http://www.premiumdxb.com/assets/img/avatar/default-avatar.jpg"                   
                };

                userManager.Create(user, "123456");
                userManager.AddToRole(user.Id, "User");
            }

            adminUser = userManager.Users.FirstOrDefault(x => x.Email == "admin@gmail.com");

            if (adminUser != null)
            {
                userManager.AddToRoles(adminUser.Id, "User", "Admin");
            }

            byte[] image = File.ReadAllBytes(@"D:\TravelAlbum\TravelAlbum.Web\Content\SingeImages\SL373229.JPG");

            SingleImage singleImage1 = new SingleImage()
            {
                Content = image,
                TravelObjectId = Guid.NewGuid(),
                CreatedOn = DateTime.Now
            };


            if (context.TravelObjects.Count() < 1)
            {
                Travel malyovitsaTravel = new Travel()
                {
                    TravelObjectId = Guid.NewGuid(),
                    StartDate = new DateTime(2017, 08, 14),
                    EndDate = new DateTime(2017, 08, 15),
                    CreatedOn = DateTime.Now,
                    Mountain = Mountain.Rila
                };

                TravelTranslationalInfo malyovitsaTravelBg = new TravelTranslationalInfo()
                {
                    TravelTranslationalInfoId = Guid.NewGuid(),
                    TravelObject = malyovitsaTravel,
                    TravelObjectId = malyovitsaTravel.TravelObjectId,
                    Language = Language.Bulgarian,
                    Title = "Мъгливата Мальовица",                    
                    Description = "адйакдйк кафйфкахй кафхйкахй кафйхйкахй кафхкйхакйфхйак айкхфакйхайкфх айхфйкафхйкха йахфйкафх фйакфхкй лкйклай какфй."
                };

                TravelTranslationalInfo malyovitsaTravelEn = new TravelTranslationalInfo()
                {
                    TravelTranslationalInfoId = Guid.NewGuid(),
                    TravelObject = malyovitsaTravel,
                    TravelObjectId = malyovitsaTravel.TravelObjectId,
                    Language = Language.English,
                    Title = "Foggy Malyovitsa",
                    Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus. Donec vitae sapien ut libero venenatis faucibus. Nullam quis ante. Etiam sit amet orci eget eros faucibus tincidunt. Duis leo. Sed fringilla mauris sit amet nibh. Donec sodales sagittis magna. Sed consequat, leo eget bibendum sodales, augue velit cursus nunc,"
                };

                byte[] malyovitsaImage1Content = File.ReadAllBytes(@"D:\TravelAlbum\TravelAlbum.Web\Content\SingeImages\SL373530.JPG");

                SingleImage malyovitsaImage1 = new SingleImage()
                {
                    Content = malyovitsaImage1Content,
                    TravelObjectId = Guid.NewGuid(),
                    CreatedOn = DateTime.Now
                };

                malyovitsaTravel.Images.Add(malyovitsaImage1);


                malyovitsaTravel.TranslatedTravels.Add(malyovitsaTravelBg);
                malyovitsaTravel.TranslatedTravels.Add(malyovitsaTravelEn);

                SingleImageTranslationalInfo singleImage1Bg = new SingleImageTranslationalInfo()
                {
                    SingleImageTranslationalInfoId = Guid.NewGuid(),
                    Description = "Sunset over Stara planina captured from Vitosha nearby Cherni vrah",
                    Language = Language.English,
                    SingleImage = singleImage1,
                    TravelObjectId = singleImage1.TravelObjectId
                };

                SingleImageTranslationalInfo singleImage1En = new SingleImageTranslationalInfo()
                {
                    SingleImageTranslationalInfoId = Guid.NewGuid(),
                    Description = "Залез над Стара планина, заснет от Витоша близо до Черни връх",
                    Language = Language.Bulgarian,
                    SingleImage = singleImage1,
                    TravelObjectId = singleImage1.TravelObjectId
                };

                singleImage1.TranslatedInfoes.Add(singleImage1Bg);
                singleImage1.TranslatedInfoes.Add(singleImage1En);

                context.SingleImageTranslationalInfoes.Add(singleImage1Bg);
                context.SingleImageTranslationalInfoes.Add(singleImage1En);
                context.TravelObjects.Add(singleImage1);
                context.TravelObjects.Add(malyovitsaImage1);
                context.TravelObjects.Add(malyovitsaTravel);
                context.TravelTranslatinalInfoes.Add(malyovitsaTravelEn);
                context.TravelTranslatinalInfoes.Add(malyovitsaTravelBg);
                context.SaveChanges();
            }
        }
    }
}
