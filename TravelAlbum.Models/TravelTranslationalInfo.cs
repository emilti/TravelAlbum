using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAlbum.Models
{
    public class TravelTranslationalInfo
    {
        [Key]
        public Guid TravelTranslationalInfoId { get; set; }

        public Guid TravelId { get; set; }
        
        [ForeignKey("TravelId")]
        public virtual Travel Travel { get; set; }
        
        // public enum Language { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
