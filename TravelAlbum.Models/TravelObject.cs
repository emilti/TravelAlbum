using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAlbum.Models
{
    public abstract class TravelObject
    {
        private ICollection<Comment> comment;

        public TravelObject()
        {           
            this.Comments = new HashSet<Comment>();
        }

        [Key]
        public Guid TravelObjectId { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comment; }
            set { this.comment = value; }
        }
    }
}
