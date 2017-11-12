using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelAlbum.Models
{
    public class SingleImage
    {
        private ICollection<SingleImageTranslationalInfo> translatedInfoes;

        private ICollection<SingleImageComment> singleImageComment;

        public SingleImage()
        {
            this.TranslatedInfoes = new HashSet<SingleImageTranslationalInfo>();  
            this.SingleImageComments = new HashSet<SingleImageComment>();
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

        public virtual ICollection<SingleImageComment> SingleImageComments
        {
            get { return this.singleImageComment; }
            set { this.singleImageComment = value; }
        }
    }
}
