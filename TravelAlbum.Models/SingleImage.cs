using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelAlbum.Models
{
    public class SingleImage
    {

        private ICollection<SingleImageTranslationalInfo> translatedInfoes;

        public SingleImage()
        {
            this.TranslatedInfoes = new HashSet<SingleImageTranslationalInfo>();            
        }

        [Key]
        public Guid SingleImageId { get; set; }

        public byte[] Content { get; set; }

        public DateTime CreatedOn { get; set; } 

        public virtual ICollection<SingleImageTranslationalInfo> TranslatedInfoes
        {
            get { return this.translatedInfoes; }
            set { this.translatedInfoes = value; }
        }
    }
}
