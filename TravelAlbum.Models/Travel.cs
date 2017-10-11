using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAlbum.Models
{
    public class Travel
    {
        private ICollection<TravelTranslationalInfo> translatedTravels;

        private ICollection<TravelImage> travelImages;

        private ICollection<ApplicationUser> usersLiked;

        public Travel()
        {
            this.translatedTravels = new HashSet<TravelTranslationalInfo>();
            this.travelImages = new HashSet<TravelImage>();
            this.UsersLiked = new HashSet<ApplicationUser>();
        }
        
        [Key]
        public Guid TravelId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Guid? MountainId { get; set; }

        [ForeignKey("MountainId")]
        public virtual Mountain Mountain { get; set; }

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

        public virtual ICollection<TravelImage> TravelImages
        {
            get { return this.travelImages; }
            set { this.travelImages = value; }
        }
    }
}
