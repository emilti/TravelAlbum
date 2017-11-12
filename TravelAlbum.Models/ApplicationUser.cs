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
        private ICollection<SingleImageComment> singleImageComments;
        
        public ApplicationUser()
        {         
            this.FavoriteTravels = new HashSet<Travel>();
            this.SingleImageComments = new HashSet<SingleImageComment>();
        }

        [Required]       
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Avatar { get; set; }
        
        [Required]
        [RegularExpression(@"[a-zA-Z_0-9]+", ErrorMessage = "Only letters, numbers and _ allowed")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string NickName { get; set; }

        public virtual ICollection<Travel> FavoriteTravels
        {
            get { return this.favoriteTravels; }
            set { this.favoriteTravels = value; }
        }


        public virtual ICollection<SingleImageComment> SingleImageComments
        {
            get { return this.singleImageComments; }
            set { this.singleImageComments = value; }
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
