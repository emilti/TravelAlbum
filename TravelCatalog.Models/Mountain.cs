using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCatalog.Models
{
    public class Mountain
    {
        private ICollection<Travel> travels;

        public Mountain()
        {
            this.Travels = new HashSet<Travel>();
        }

        [Key]
        public Guid Id { get; set; }

        public virtual ICollection<Travel> Travels
        {
            get { return this.travels; }
            set { this.travels = value; }
        }

    }
}
