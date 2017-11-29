using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelAlbum.Models
{
    public class Mountain
    {
        private ICollection<SingleImage> singleImages;

        public Mountain()
        {
            this.SingleImages = new HashSet<SingleImage>();
        }

        [Key]
        public Guid MountainId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<SingleImage> SingleImages
        {
            get { return this.singleImages; }
            set { this.singleImages = value; }
        }
    }
}
