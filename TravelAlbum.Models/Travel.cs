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

        private ICollection<Image> images;
        
        private ICollection<ApplicationUser> usersLiked;

        public Travel()
        {
            this.translatedTravels = new HashSet<TravelTranslationalInfo>();
            this.images = new HashSet<Image>();
            this.UsersLiked = new HashSet<ApplicationUser>();
        }
        
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

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

        public virtual ICollection<Image> Images
        {
            get { return this.images; }
            set { this.images = value; }
        }
    }
}
