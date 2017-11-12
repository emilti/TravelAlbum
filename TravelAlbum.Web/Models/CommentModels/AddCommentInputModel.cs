﻿using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TravelAlbum.Web.Models.CommentModels
{
    public class AddCommentInputModel
    {
        [Required]
        [AllowHtml]
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "Invalid symbol")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Content { get; set; }
    }
}