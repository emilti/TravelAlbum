namespace TravelAlbum.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
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

            if (context.TravelObjects.Count() < 1)
            {
                Mountain rila = new Mountain()
                {
                    MountainId = Guid.NewGuid(),
                    Name = "Rila"
                };

                Mountain vitosha = new Mountain()
                {
                    MountainId = Guid.NewGuid(),
                    Name = "Vitosha"
                };

                Mountain pirin = new Mountain()
                {
                    MountainId = Guid.NewGuid(),
                    Name = "Pirin"
                };

                MountainTranslationalInfo vitoshaBg = new MountainTranslationalInfo()
                {
                    MountainTranslationalInfoId = Guid.NewGuid(),
                    Mountain = vitosha,
                    MountainId = vitosha.MountainId,
                    Language = Language.Bulgarian,
                    Name = "Витоша"
                };

                MountainTranslationalInfo vitoshaEn = new MountainTranslationalInfo()
                {
                    MountainTranslationalInfoId = Guid.NewGuid(),
                    Mountain = vitosha,
                    MountainId = vitosha.MountainId,
                    Language = Language.English,
                    Name = "Vitosha"
                };

                MountainTranslationalInfo rilaBg = new MountainTranslationalInfo()
                {
                    MountainTranslationalInfoId = Guid.NewGuid(),
                    Mountain = rila,
                    MountainId = rila.MountainId,
                    Language = Language.Bulgarian,
                    Name = "Рила"
                };

                MountainTranslationalInfo rilaEn = new MountainTranslationalInfo()
                {
                    MountainTranslationalInfoId = Guid.NewGuid(),
                    Mountain = rila,
                    MountainId = rila.MountainId,
                    Language = Language.English,
                    Name = "Rila"
                };

                MountainTranslationalInfo pirinBg = new MountainTranslationalInfo()
                {
                    MountainTranslationalInfoId = Guid.NewGuid(),
                    Mountain = pirin,
                    MountainId = pirin.MountainId,
                    Language = Language.Bulgarian,
                    Name = "Пирин"
                };

                MountainTranslationalInfo pirinEn = new MountainTranslationalInfo()
                {
                    MountainTranslationalInfoId = Guid.NewGuid(),
                    Mountain = pirin,
                    MountainId = pirin.MountainId,
                    Language = Language.English,
                    Name = "Pirin"
                };

                context.MountainTranslationalInfoes.Add(vitoshaBg);
                context.MountainTranslationalInfoes.Add(vitoshaEn);
                context.MountainTranslationalInfoes.Add(rilaBg);
                context.MountainTranslationalInfoes.Add(rilaEn);
                context.MountainTranslationalInfoes.Add(pirinBg);
                context.MountainTranslationalInfoes.Add(pirinEn);


                Travel malyovitsaTravel = new Travel()
                {
                    TravelObjectId = Guid.NewGuid(),
                    StartDate = new DateTime(2017, 08, 14),
                    EndDate = new DateTime(2017, 08, 15),
                    CreatedOn = DateTime.Now                    
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

                var basePath = AppDomain.CurrentDomain.BaseDirectory;
               
                byte[] malyovitsaImage1Content = File.ReadAllBytes(basePath + @"/Content/DBImages/SL373530.JPG");
                byte[] malyovitsaImage1PreviewContent = File.ReadAllBytes(basePath + @"/Content/DBImages/SL373530_preview.JPG");


                Image malyovitsaImage1 = new Image()
                {
                    Content = malyovitsaImage1Content,
                    PreviewContent = malyovitsaImage1PreviewContent,
                    TravelObjectId = Guid.NewGuid(),
                    CreatedOn = new DateTime(2017, 8, 15),
                    Mountain = rila,
                    MountainId = rila.MountainId                    
                };


                rila.Images.Add(malyovitsaImage1);

                byte[] imageContent = File.ReadAllBytes(basePath + @"/Content/DBImages/SL373229.JPG");
                byte[] imagePreviewContent = File.ReadAllBytes(basePath + @"/Content/DBImages/SL373229_preview.JPG");


                Image image1 = new Image()
                {
                    Content = imageContent,
                    PreviewContent = imagePreviewContent,
                    TravelObjectId = Guid.NewGuid(),
                    CreatedOn = new DateTime(2017, 6, 9),
                    Mountain = vitosha,
                    MountainId = vitosha.MountainId                    
                };

                vitosha.Images.Add(image1);

                byte[] image2Content = File.ReadAllBytes(basePath + @"/Content/DBImages/SL373233.JPG");
                byte[] previewImage2Content = File.ReadAllBytes(basePath + @"/Content/DBImages/SL373233_preview.JPG");

                Image image2 = new Image()
                {
                    Content = image2Content,
                    PreviewContent = previewImage2Content,
                    TravelObjectId = Guid.NewGuid(),
                    CreatedOn = new DateTime(2017, 6, 9),
                    Mountain = vitosha,
                    MountainId = vitosha.MountainId                    
                };

                vitosha.Images.Add(image2);

                byte[] image3Content = File.ReadAllBytes(basePath + @"/Content/DBImages/SL373235.JPG");
                byte[] previewImage3Content = File.ReadAllBytes(basePath + @"/Content/DBImages/SL373235_preview.JPG");

                Image image3 = new Image()
                {
                    Content = image3Content,
                    PreviewContent = previewImage3Content,
                    TravelObjectId = Guid.NewGuid(),
                    CreatedOn = new DateTime(2017, 6, 9),
                    Mountain = vitosha,
                    MountainId = vitosha.MountainId                    
                };

                vitosha.Images.Add(image3);


                byte[] image4Content = File.ReadAllBytes(basePath + @"/Content/DBImages/SL373481.JPG");
                byte[] previewImage4Content = File.ReadAllBytes(basePath + @"/Content/DBImages/SL373481_preview.JPG");

                Image image4 = new Image()
                {
                    Content = image4Content,
                    PreviewContent = previewImage4Content,
                    TravelObjectId = Guid.NewGuid(),
                    CreatedOn = new DateTime(2017, 7, 23),
                    Mountain = rila,
                    MountainId = rila.MountainId
                };

                rila.Images.Add(image4);

                byte[] image5Content = File.ReadAllBytes(basePath + @"/Content/DBImages/SL373668.JPG");
                byte[] previewImage5Content = File.ReadAllBytes(basePath + @"/Content/DBImages/SL373668_preview.JPG");

                Image image5 = new Image()
                {
                    Content = image5Content,
                    PreviewContent = previewImage5Content,
                    TravelObjectId = Guid.NewGuid(),
                    CreatedOn = new DateTime(2017, 9, 2),
                    Mountain = rila,
                    MountainId = rila.MountainId
                };

                rila.Images.Add(image5);
                byte[] image6Content = File.ReadAllBytes(basePath + @"/Content/DBImages/SL373688.JPG");
                byte[] previewImage6Content = File.ReadAllBytes(basePath + @"/Content/DBImages/SL373688_preview.JPG");

                Image image6 = new Image()
                {
                    Content = image6Content,
                    PreviewContent = previewImage6Content,
                    TravelObjectId = Guid.NewGuid(),
                    CreatedOn = new DateTime(2017, 9, 2),
                    Mountain = rila,
                    MountainId = rila.MountainId
                };

                rila.Images.Add(image6);

                context.Mountains.Add(rila);
                context.Mountains.Add(pirin);
                context.Mountains.Add(vitosha);

                malyovitsaTravel.Images.Add(malyovitsaImage1);


                malyovitsaTravel.TranslatedTravels.Add(malyovitsaTravelBg);
                malyovitsaTravel.TranslatedTravels.Add(malyovitsaTravelEn);

                ImageTranslationalInfo image1Bg = new ImageTranslationalInfo()
                {
                    ImageTranslationalInfoId = Guid.NewGuid(),
                    Description = "Sunset over Stara planina captured from Vitosha nearby Cherni vrah",
                    Language = Language.English,
                    Image = image1,
                    TravelObjectId = image1.TravelObjectId
                };

                ImageTranslationalInfo image1En = new ImageTranslationalInfo()
                {
                    ImageTranslationalInfoId = Guid.NewGuid(),
                    Description = "Залез над Стара планина, заснет от Витоша близо до Черни връх",
                    Language = Language.Bulgarian,
                    Image = image1,
                    TravelObjectId = image1.TravelObjectId
                };

                image1.TranslatedInfoes.Add(image1Bg);
                image1.TranslatedInfoes.Add(image1En);

                context.ImageTranslationalInfoes.Add(image1Bg);
                context.ImageTranslationalInfoes.Add(image1En);
                context.TravelObjects.Add(image1);
                context.TravelObjects.Add(image2);
                context.TravelObjects.Add(image3);
                context.TravelObjects.Add(image4);
                context.TravelObjects.Add(image5);
                context.TravelObjects.Add(image6);
                context.TravelObjects.Add(malyovitsaImage1);
                context.TravelObjects.Add(malyovitsaTravel);
                context.TravelTranslatinalInfoes.Add(malyovitsaTravelEn);
                context.TravelTranslatinalInfoes.Add(malyovitsaTravelBg);
                context.SaveChanges();
            }
        }
    }
}
