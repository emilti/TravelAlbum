using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelCatalog.Models
{
    public class Travel_Translated
    {
        public Guid Id { get; set; }

        public Guid TravelId { get; set; }

        [ForeignKey("TravelId")]
        public virtual Travel Travel { get; set; }

        public Guid LanguageId { get; set; }

        [ForeignKey("LanguageId")]
        public virtual Language Language { get; set; }
    }
}
