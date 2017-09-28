using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAlbum.Models
{
    public class TravelTranslationalInfo
    {
        public Guid Id { get; set; }

        public Guid TravelId { get; set; }

        [ForeignKey("TravelId")]
        public virtual Travel Travel { get; set; }
        
        public Languages Language { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
