using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAlbum.Models
{
    public class ImageTranslationalInfo
    {
        public Guid ImageTranslationalInfoId { get; set;}

        public Guid TravelObjectId { get; set; }

        [ForeignKey("TravelObjectId")]
        public virtual Image Image { get; set; }
        
        public string Title { get; set; }

        public string Description { get; set; }

        public Language Language { get; set; }
    }
}
