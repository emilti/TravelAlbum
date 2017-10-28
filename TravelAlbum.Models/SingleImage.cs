using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAlbum.Models
{
    public class SingleImage
    {
        [Key]
        public Guid SingleImageId { get; set; }

        public byte[] Content { get; set; }
    }
}
