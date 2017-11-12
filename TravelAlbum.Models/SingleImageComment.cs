using System;
using System.ComponentModel.DataAnnotations;

namespace TravelAlbum.Models
{
    public class SingleImageComment
    {
        [Key]
        public Guid SingleImageCommentId { get; set; }

        [Required]        
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Invalid symbol")]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Content { get; set; }

        public Guid SingleImageId { get; set; }

        public virtual SingleImage SingleImage { get; set; }

        public string AuthorName { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public virtual DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}
