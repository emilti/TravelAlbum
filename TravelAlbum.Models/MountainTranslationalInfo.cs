using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAlbum.Models
{
    public class MountainTranslationalInfo
    {
        public Guid MountainTranslationalInfoId { get; set; }

        public Guid MountainId { get; set; }

        [ForeignKey("MountainId")]
        public virtual Mountain Mountain { get; set; }

        public string Name { get; set; }

        public Language Language { get; set; }
    }
}
