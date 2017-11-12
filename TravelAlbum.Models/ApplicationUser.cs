using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TravelAlbum.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {       
        private ICollection<Travel> favoriteTravels;
        
        public ApplicationUser()
        {         
            this.FavoriteTravels = new HashSet<Travel>();            
        }

        [Required]        
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Invalid symbol")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Avatar { get; set; }

        [Required]       
        [RegularExpression(@"[a-zA-Z_0-9]+", ErrorMessage = "Only letters, numbers and _ allowed")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]        
        [RegularExpression(@"[a-zA-Z_0-9]+", ErrorMessage = "Only letters, numbers and _ allowed")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string LastName { get; set; }

        public virtual ICollection<Travel> FavoriteTravels
        {
            get { return this.favoriteTravels; }
            set { this.favoriteTravels = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }      
    }
}
