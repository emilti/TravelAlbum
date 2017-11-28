using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAlbum.Models
{

    [Table("Travel")]
    public class Travel : TravelObject
    {
        private ICollection<TravelTranslationalInfo> translatedTravels;

        private ICollection<SingleImage> images;


        private ICollection<ApplicationUser> usersLiked;

        public Travel()
        {
            this.translatedTravels = new HashSet<TravelTranslationalInfo>();
            this.images = new HashSet<SingleImage>();
            this.UsersLiked = new HashSet<ApplicationUser>();
        }
        
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public Mountain Mountain { get; set; }        

        [InverseProperty("FavoriteTravels")]
        public virtual ICollection<ApplicationUser> UsersLiked
        {
            get { return this.usersLiked; }
            set { this.usersLiked = value; }
        }        

        public virtual ICollection<TravelTranslationalInfo> TranslatedTravels
        {
            get { return this.translatedTravels; }
            set { this.translatedTravels = value; }
        }

        public virtual ICollection<SingleImage> Images
        {
            get { return this.images; }
            set { this.images = value; }
        }
    }
}
