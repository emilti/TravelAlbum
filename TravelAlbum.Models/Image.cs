
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAlbum.Models
{

    [Table("Image")]
    public class Image : TravelObject
    {
        private ICollection<ImageTranslationalInfo> translatedInfoes;

        public Image()
        {
            this.TranslatedInfoes = new HashSet<ImageTranslationalInfo>();             
        }       

        public byte[] Content { get; set; }

        public byte[] PreviewContent { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
        
        public Guid? TravelId { get; set; }

        [ForeignKey("TravelId")]
        public virtual Travel Travel { get; set; }

        public Guid? MountainId { get; set; }

        [ForeignKey("MountainId")]
        public virtual Mountain Mountain { get; set; }       

        public virtual ICollection<ImageTranslationalInfo> TranslatedInfoes
        {
            get { return this.translatedInfoes; }
            set { this.translatedInfoes = value; }
        }       
    }
}
