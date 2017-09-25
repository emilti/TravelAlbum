using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCatalog.Models
{
    public class Travel
    {
        public Guid Id { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }



       // [InverseProperty("FavoriteTravels")]
       // public virtual ICollection<AppUser> UsersLiked
       // {
       //     get { return this.usersLiked; }
       //     set { this.usersLiked = value; }
       // }

        public string Title { get; set; }
        
        public string Description { get; set; }
    }
}
