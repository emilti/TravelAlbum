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
                    EndDate = new DateTime(2017, 08, 15)                                     
                };

                TravelTranslationalInfo malyovitsaTravelBg = new TravelTranslationalInfo()
                {
                    TravelTranslationalInfoId = Guid.NewGuid(),
                    TravelObject = malyovitsaTravel,
                    TravelObjectId = malyovitsaTravel.TravelObjectId,
                    Language = Language.Bulgarian,
                    Title = "Почти до връх Мальовица",                    
                    Description =
                    "&emsp;Лошото време в средата на август провали планивете ми за няколко дневен излет в Рила, но в крайна сметка реших да не се отказвам и вместо да направя дълъг преход от 7-те рилски езера до Боровец съкратих маршрута си. Крайната цел на екскурзията беше хижа Мальовица, като планирах да тръгна от началната станция на лифта при 7-те рилски езера, да стигна до Раздела и от там по билото на Урдиния циркус да стигна до хижа Мальовица. Бях ходил до 7-те езера и Раздела, но не бях минавал по този път към хижа Мальовица. Обмислях два варианта за прибиране – Урдиния циркус, по който съм дошъл или от хижа Мальовица да продължа до Централна планинска школа и от там по пътеката за хижа Вада да стигна обратно до началната станция на лифта при 7-те рилски езера, като на място щях да избера по-подходящия./img1/" +
                    "&emsp;Към 11 часа сутринта пристигнах на началаната станция на лифта при хижа Пионерска. Въпреки облачното време се беше образувала опашка. Хванах пътечката, " +
                    "която започва на около петедесетина метра вдясно от началната станция и с добро темпо започнах да се придвижвам към езерата. За по-малко от час  стигнах до горната станция. Постепенно започна да се появява мъгла. Пътечката, " +
                    "по която вървах към Езерата, " +
                    "представлява черен път, " +
                    "който за съжаление се използва от местните за превоз на туристи така, " +
                    "че ако човек има непоносимост към шумни, " +
                    "вдигащи прах джипове, " +
                    "е по-добре да търси друг вариант за придвижване до 7-те рилски езера. След по-малко от час стигнах до новата хижа 7-те рилски езера. Хапнах набързо една пилешка супа и с нови сили тръгнах към езерото Бъбрека. Имаше много туристи, " +
                    "като доста от тях бяха от чужбина – френска, " +
                    "немска и някаква източно - европейска реч, " +
                    "може би чешка, " +
                    "преобладаваха. Пътят, " +
                    "който води към Бъбрека, " +
                    "предлага панорамна гледка към езерата Дoлното и Рибното, " +
                    "но облаците, " +
                    "които се спускаха, " +
                    "скриваха от части красотата, " +
                    "която се открива от това място. Стигнах до Бъбрека и отново имах чувството, " +
                    "че съм в София, " +
                    "заради огромното количество хора около езерото. Набързо подминах Бъбрека, " +
                    "а след това и Окото и Сълзата. На Сълзата имах кратък разговор с един англичанин. Питаше дали няма друг път за връщане, " +
                    "с който да обиколи малко повече, " +
                    "но имайки предвид че беше по дънки и сандали го посъветвах да се върне по пътя, " +
                    "по който е дошъл/img2/. Стигнах Раздела от там от Урдините езера не се виждаше нищо, " +
                    "но пък се откриваше отлична панорама към масива на Калините. Направих няколко снимки и потеглих нататък. За съжаление мъглата започна да се спуска все повече и от Дамга до Додов връх не се виждаше нищо на повече от няколко десетки метра, " +
                    "Изкачих Мермерите и реших да не се отклонявам до връх Мальовица, " +
                    "защото така или иначе нищо не се виждаше на повече от 5-10 метра. В един момент в близост чух някакви гласове от малка група, " +
                    "но така и не срещнах никого. Вниманието си бях насочил да не изтърва разклона за хижа Мальовица, " +
                    "защото в един момент пътеката се разделя на две и левият път продължава към хижата, " +
                    "а десният продължаваше към спане на открито. След неприятно слизане по камънаците стигнах до Еленино езеро, " +
                    "където имаше руска група, " +
                    "опънала палатки. Започна лека-полека да се свечерява и към 7 и половина стигнах хижа Мальовица. Хижата беше пълна, " +
                    "хапнах и доволен легнах да спя./img3/" +
                    "&emsp;Поредното трудно ранно ставане, " +
                    "но няма как – трябваше да се прибирам в София. Избрах да мина през ЦПШ и хижа Вада до началната станция на лифта при хижа Пионерска. Това разстояние отнема 3-4 часа. През по-голямата част от пътя времето беше отлично. За съжаление този маршрут не ми е от любимите, " +
                    "защото преминава изцяло през гора и няма много какво да се види. Самият път е добре обозначен. Когато се ходи от ЦПШ към хижа Пионерска има едно отклонение вляво след Яворова поляна, " +
                    "за което трябва да се внимава. " +
                    "След няколко часа ходене стигнах до началнато станция на лифта, " +
                    "а от там и поех по обратния път за София."
                };

                TravelTranslationalInfo malyovitsaTravelEn = new TravelTranslationalInfo()
                {
                    TravelTranslationalInfoId = Guid.NewGuid(),
                    TravelObject = malyovitsaTravel,
                    TravelObjectId = malyovitsaTravel.TravelObjectId,
                    Language = Language.English,
                    Title = "To Malyovitsa peak almost",
                    Description = "Not translated"
                };

                var basePath = AppDomain.CurrentDomain.BaseDirectory;

                byte[] malyovitsaImage1Content = File.ReadAllBytes(basePath + @"/Content/DBImages/SL373490.JPG");
                byte[] malyovitsaImage1PreviewContent = File.ReadAllBytes(basePath + @"/Content/DBImages/SL373490.JPG");
                
                Image malyovitsaImage1 = new Image()
                {
                    Content = malyovitsaImage1Content,
                    PreviewContent = malyovitsaImage1PreviewContent,
                    TravelObjectId = Guid.NewGuid(),
                    Travel = malyovitsaTravel,
                    TravelId = malyovitsaTravel.TravelObjectId,
                    CreatedOn = new DateTime(2017, 8, 14),
                    Mountain = rila,
                    MountainId = rila.MountainId                    
                };


                rila.Images.Add(malyovitsaImage1);

                byte[] malyovitsaImage2Content = File.ReadAllBytes(basePath + @"/Content/DBImages/SL373498.JPG");
                byte[] malyovitsaImage2PreviewContent = File.ReadAllBytes(basePath + @"/Content/DBImages/SL373498.JPG");

                Image malyovitsaImage2 = new Image()
                {
                    Content = malyovitsaImage2Content,
                    PreviewContent = malyovitsaImage2PreviewContent,
                    TravelObjectId = Guid.NewGuid(),
                    Travel = malyovitsaTravel,
                    TravelId = malyovitsaTravel.TravelObjectId,
                    CreatedOn = new DateTime(2017, 8, 14),
                    Mountain = rila,
                    MountainId = rila.MountainId,
                    TravelLabel = "/img1/"
                };


                rila.Images.Add(malyovitsaImage2);

                byte[] malyovitsaImage3Content = File.ReadAllBytes(basePath + @"/Content/DBImages/SL373503.JPG");
                byte[] malyovitsaImage3PreviewContent = File.ReadAllBytes(basePath + @"/Content/DBImages/SL373503.JPG");

                Image malyovitsaImage3 = new Image()
                {
                    Content = malyovitsaImage3Content,
                    PreviewContent = malyovitsaImage3PreviewContent,
                    TravelObjectId = Guid.NewGuid(),
                    Travel = malyovitsaTravel,
                    TravelId = malyovitsaTravel.TravelObjectId,
                    CreatedOn = new DateTime(2017, 8, 14),
                    Mountain = rila,
                    MountainId = rila.MountainId
                };

                rila.Images.Add(malyovitsaImage3);

                byte[] malyovitsaImage4Content = File.ReadAllBytes(basePath + @"/Content/DBImages/SL373514.JPG");
                byte[] malyovitsaImage4PreviewContent = File.ReadAllBytes(basePath + @"/Content/DBImages/SL373514.JPG");

                Image malyovitsaImage4 = new Image()
                {
                    Content = malyovitsaImage4Content,
                    PreviewContent = malyovitsaImage4PreviewContent,
                    TravelObjectId = Guid.NewGuid(),
                    Travel = malyovitsaTravel,
                    TravelId = malyovitsaTravel.TravelObjectId,
                    CreatedOn = new DateTime(2017, 8, 14),
                    Mountain = rila,
                    MountainId = rila.MountainId,
                    TravelLabel = "/img2/"
                };

                rila.Images.Add(malyovitsaImage4);

                byte[] malyovitsaImage5Content = File.ReadAllBytes(basePath + @"/Content/DBImages/SL373530.JPG");
                byte[] malyovitsaImage5PreviewContent = File.ReadAllBytes(basePath + @"/Content/DBImages/SL373530_preview.JPG");


                Image malyovitsaImage5 = new Image()
                {
                    Content = malyovitsaImage5Content,
                    PreviewContent = malyovitsaImage5PreviewContent,
                    TravelObjectId = Guid.NewGuid(),
                    Travel = malyovitsaTravel,
                    TravelId = malyovitsaTravel.TravelObjectId,
                    CreatedOn = new DateTime(2017, 8, 15),
                    Mountain = rila,
                    MountainId = rila.MountainId,
                    TravelLabel = "/img3/"
                };


                rila.Images.Add(malyovitsaImage5);

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
                malyovitsaTravel.Images.Add(malyovitsaImage2);
                malyovitsaTravel.Images.Add(malyovitsaImage3);
                malyovitsaTravel.Images.Add(malyovitsaImage4);
                malyovitsaTravel.Images.Add(malyovitsaImage5);


                malyovitsaTravel.TranslatedTravels.Add(malyovitsaTravelBg);
                malyovitsaTravel.TranslatedTravels.Add(malyovitsaTravelEn);

                ImageTranslationalInfo image1Bg = new ImageTranslationalInfo()
                {
                    ImageTranslationalInfoId = Guid.NewGuid(),
                    Title = "Konyarnika, Vitosha",
                    Description = "Sunset over Stara planina captured from Vitosha nearby Cherni vrah",
                    Language = Language.English,
                    Image = image1,
                    TravelObjectId = image1.TravelObjectId
                };

                ImageTranslationalInfo image1En = new ImageTranslationalInfo()
                {
                    ImageTranslationalInfoId = Guid.NewGuid(),
                    Title = "Конярника, Витоша",
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
                context.TravelObjects.Add(malyovitsaImage2);
                context.TravelObjects.Add(malyovitsaImage3);
                context.TravelObjects.Add(malyovitsaImage4);
                context.TravelObjects.Add(malyovitsaImage5);
                context.TravelObjects.Add(malyovitsaTravel);
                context.TravelTranslatinalInfoes.Add(malyovitsaTravelEn);
                context.TravelTranslatinalInfoes.Add(malyovitsaTravelBg);
                context.SaveChanges();
            }
        }
    }
}
