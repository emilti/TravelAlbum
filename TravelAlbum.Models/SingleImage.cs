using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAlbum.Models
{

    [Table("SingleImage")]
    public class SingleImage : TravelObject
    {
        private ICollection<SingleImageTranslationalInfo> translatedInfoes;

        public SingleImage()
        {
            this.TranslatedInfoes = new HashSet<SingleImageTranslationalInfo>();             
        }       

        public byte[] Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid? TravelId { get; set; }

        [ForeignKey("TravelId")]
        public virtual Travel Travel { get; set; }

        public Guid? MountainId { get; set; }

        [ForeignKey("MountainId")]
        public virtual Mountain Mountain { get; set; }

        public virtual ICollection<SingleImageTranslationalInfo> TranslatedInfoes
        {
            get { return this.translatedInfoes; }
            set { this.translatedInfoes = value; }
        }       
    }
}
