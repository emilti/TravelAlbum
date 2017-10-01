using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAlbum.Models
{
    public class TravelImage
    { 
        public Guid Id { get; set; }

        public Guid? TravelId { get; set; }

        [ForeignKey("TravelId")]
        public virtual Travel Travel { get; set; }

        public byte[] Content { get; set; }
    }
}
