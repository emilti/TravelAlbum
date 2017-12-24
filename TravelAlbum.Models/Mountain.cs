using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelAlbum.Models
{
    public class Mountain
    {
        private ICollection<SingleImage> singleImages;

        private ICollection<MountainTranslationalInfo> translatedInfoes;      

        public Mountain()
        {
            this.SingleImages = new HashSet<SingleImage>();
            this.TranslatedInfoes = new HashSet<MountainTranslationalInfo>();
        }

        [Key]
        public Guid MountainId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<SingleImage> SingleImages
        {
            get { return this.singleImages; }
            set { this.singleImages = value; }
        }

        public virtual ICollection<MountainTranslationalInfo> TranslatedInfoes
        {
            get { return this.translatedInfoes; }
            set { this.translatedInfoes = value; }
        }
    }
}
