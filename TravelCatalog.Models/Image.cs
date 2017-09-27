using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelCatalog.Models
{
    public class Image
    { 
        public Guid Id { get; set; }

        public int Content { get; set; }

        public Guid? TravelId { get; set; }

        [ForeignKey("TravelId")]
        public virtual Travel Travel { get; set; }
    }
}
