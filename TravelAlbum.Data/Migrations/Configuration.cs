namespace TravelAlbum.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
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
        }
    }
}
