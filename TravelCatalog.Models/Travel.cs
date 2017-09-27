using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelCatalog.Models
{
    public class Travel
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
        
        public Guid Id { get; set; }

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

        public virtual ICollection<Image> Images
        {
            get { return this.images; }
            set { this.images = value; }
        }
    }
}
