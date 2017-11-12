using System.Collections.Generic;
using TravelAlbum.Web.Models.CommentModels;

namespace TravelAlbum.Web.Models.BatchCommentsModels
{
    public class BatchCommentsViewModel
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public List<CommentViewModel> Comments { get; set; }
    }
}