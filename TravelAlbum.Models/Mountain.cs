using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelAlbum.Models
{
    public class Mountain
    {
        private ICollection<Image> images;

        private ICollection<MountainTranslationalInfo> translatedInfoes;      

        public Mountain()
        {
            this.Images = new HashSet<Image>();
            this.TranslatedInfoes = new HashSet<MountainTranslationalInfo>();
        }

        [Key]
        public Guid MountainId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Image> Images
        {
            get { return this.images; }
            set { this.images = value; }
        }

        public virtual ICollection<MountainTranslationalInfo> TranslatedInfoes
        {
            get { return this.translatedInfoes; }
            set { this.translatedInfoes = value; }
        }
    }
}
