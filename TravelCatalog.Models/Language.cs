using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCatalog.Models
{
    public class Language
    {
        private ICollection<Travel_Translated> translatedTravels;

        public Language()
        {
            this.translatedTravels = new HashSet<Travel_Translated>();
        }

        public Guid Id { get; set; }

        public string Code { get; set; }

        public virtual ICollection<Travel_Translated> TranslatedTravels
        {
            get { return this.translatedTravels; }
            set { this.translatedTravels = value; }
        }
    }
}
