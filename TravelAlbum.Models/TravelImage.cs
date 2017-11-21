using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAlbum.Models
{
    public class TravelImage
    {
        [Key]
        public Guid TravelImageId { get; set; }

        public Guid TravelObject { get; set; }

        [ForeignKey("TravelObject")]
        public virtual Travel Travel { get; set; }

        public byte[] Content { get; set; }
    }
}
