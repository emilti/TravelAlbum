using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using TravelAlbum.Data.Contracts;
using TravelAlbum.Models;
using System;

namespace TravelAlbum.Data
{
    public class TravelAlbumEfDbContext : IdentityDbContext<ApplicationUser>
    {

        public TravelAlbumEfDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }


        public static TravelAlbumEfDbContext Create()
        {
            return new TravelAlbumEfDbContext();
        }

        public virtual IDbSet<Travel> Travels { get; set; }

        public virtual IDbSet<TravelTranslationalInfo> TravelTranslatinalInfoes { get; set; }

        public virtual IDbSet<TravelImage> Images { get; set; }        

        public virtual IDbSet<Mountain> Mountains { get; set; }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public override int SaveChanges()
        {           
            return base.SaveChanges();
        }

        //Identity and Authorization
        // public DbSet<UserLogin> UserLogins { get; set; }
        // public DbSet<UserClaim> UserClaims { get; set; }
        // public DbSet<UserRole> UserRoles { get; set; }
        //
        // // ... your custom DbSets
        // public DbSet<RoleOperation> RoleOperations { get; set; }
        //
        // protected override void OnModelCreating(DbModelBuilder modelBuilder)
        // {
        //     base.OnModelCreating(modelBuilder);
        //
        //     modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //     modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        //
        //     // Configure Asp Net Identity Tables
        //     modelBuilder.Entity<User>().ToTable("User");
        //     modelBuilder.Entity<User>().Property(u => u.PasswordHash).HasMaxLength(500);
        //     modelBuilder.Entity<User>().Property(u => u.Stamp).HasMaxLength(500);
        //     modelBuilder.Entity<User>().Property(u => u.PhoneNumber).HasMaxLength(50);
        //
        //     modelBuilder.Entity<Role>().ToTable("Role");
        //     modelBuilder.Entity<UserRole>().ToTable("UserRole");
        //     modelBuilder.Entity<UserLogin>().ToTable("UserLogin");
        //     modelBuilder.Entity<UserClaim>().ToTable("UserClaim");
        //     modelBuilder.Entity<UserClaim>().Property(u => u.ClaimType).HasMaxLength(150);
        //     modelBuilder.Entity<UserClaim>().Property(u => u.ClaimValue).HasMaxLength(500);
        // }
    }
}
