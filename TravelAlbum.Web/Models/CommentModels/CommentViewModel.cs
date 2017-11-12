using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using TravelAlbum.Models;

namespace TravelAlbum.Web.Models.CommentModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Invalid symbol")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Email { get; set; }

        public string AuthorName { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public bool IsDeleted { get; set; }       
    }
}