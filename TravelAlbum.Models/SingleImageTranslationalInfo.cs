using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAlbum.Models
{
    public class SingleImageTranslationalInfo
    {
        public Guid SingleImageTranslationalInfoId { get; set;}

        public Guid TravelObjectId { get; set; }

        [ForeignKey("TravelObjectId")]
        public virtual SingleImage SingleImage { get; set; }

        public string Description { get; set; }

        public Language Language { get; set; }
    }
}
