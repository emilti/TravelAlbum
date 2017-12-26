using Bytes2you.Validation;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Models;
using TravelAlbum.Web.Models.BatchCommentsModels;
using TravelAlbum.Web.Models.CommentModels;

namespace TravelAlbum.Web.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentsService commentsService;
        private readonly IImageService imageService;
        private readonly IApplicationUserService usersService;
        private readonly ITravelObjectService travelObjectService;
        private readonly ITravelService travelService;

        public CommentsController(ICommentsService commentsService, IImageService imageService, IApplicationUserService usersService, ITravelObjectService travelObjectService, ITravelService travelService)
        {
            Guard.WhenArgument(commentsService, "commentsService").IsNull().Throw();
            Guard.WhenArgument(imageService, "imageService").IsNull().Throw();
            Guard.WhenArgument(usersService, "usersService").IsNull().Throw();
            Guard.WhenArgument(travelObjectService, "travelObjectService").IsNull().Throw();
            Guard.WhenArgument(travelService, "travelService").IsNull().Throw();

            this.commentsService = commentsService;
            this.imageService = imageService;
            this.usersService = usersService;
            this.travelObjectService = travelObjectService;
            this.travelService = travelService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult AddComment(int id)
        {
            this.ViewBag.ImageId = id;
            return this.View();
        }

        [HttpGet]        
        public ActionResult ShowBatchComments(Guid id, int page = 1)
        {
            TravelObject travelObject = this.travelObjectService.GetById(id);

            List<Comment> comments =
                travelObject.Comments.AsQueryable().OrderByDescending(x => x.CreatedOn).Take(5 * page).ToList();

            

            BatchCommentsViewModel batchCommentsViewModels = new BatchCommentsViewModel()
            {
                CurrentPage = page,
                Comments = new List<CommentViewModel>()
            };

            for (int i = 0; i < comments.Count; i++)
            {
                CommentViewModel commentViewModel = new CommentViewModel()
                {
                    Author = comments[i].Author,
                    AuthorId = comments[i].AuthorId,
                    AuthorName = comments[i].AuthorName,
                    Content = comments[i].Content,
                    CreatedOn = comments[i].CreatedOn,
                    IsDeleted = comments[i].IsDeleted                    
                };

                batchCommentsViewModels.Comments.Add(commentViewModel);
            }

            return this.PartialView("_BatchCommentsPartial", batchCommentsViewModels);
        }

        //TODO: Make one single method 
        // [HttpPost]
        // [Authorize]
        // public ActionResult AddComment(Guid id, string content)
        // {
        //     if (this.ModelState.IsValid)
        //     {
        //         // FacilityComment mappedComment = AutoMapperConfig.Configuration.CreateMapper().Map<FacilityComment>(model);
        //         Image commentedSingleImage = this.ImageService.GetById(id);
        //         ApplicationUser user = this.usersService.GetUserDetails(this.User.Identity.GetUserId());
        //         string username = user.UserName;
        //         Comment singleImageComment = new Comment()
        //         {
        //             CommentId = Guid.NewGuid(),
        //             Content = content,
        //             Author = user,
        //             AuthorId = user.Id,
        //             AuthorName = username,
        //             TravelObject = commentedSingleImage,
        //             TravelObjectId = commentedSingleImage.TravelObjectId,
        //             CreatedOn = DateTime.Now,
        //             IsDeleted = false
        //         };
        // 
        //         this.commentsService.Add(singleImageComment);
        //         return this.RedirectToAction("Details", "SingleImages", new { id = id });
        //     }
        // 
        //     return this.RedirectToAction("Details", "SingleImages", new { id = id });
        // }


        [HttpPost]
        [Authorize]
        public ActionResult AddComment(Guid id, string content, string controller)
        {

            if (this.ModelState.IsValid)
            {
                // FacilityComment mappedComment = AutoMapperConfig.Configuration.CreateMapper().Map<FacilityComment>(model);
                TravelObject commentedTravel = this.travelObjectService.GetById(id);
                ApplicationUser user = this.usersService.GetUserDetails(this.User.Identity.GetUserId());
                string username = user.UserName;
                Comment travelComment = new Comment()
                {
                    CommentId = Guid.NewGuid(),
                    Content = content,
                    Author = user,
                    AuthorId = user.Id,
                    AuthorName = username,
                    TravelObject = commentedTravel,
                    TravelObjectId = commentedTravel.TravelObjectId,
                    CreatedOn = DateTime.Now,
                    IsDeleted = false
                };

                this.commentsService.Add(travelComment);
                return this.RedirectToAction("Details", controller, new { id = id });
            }

            return this.RedirectToAction("Details", controller, new { id = id });
        }

        // [HttpGet]
        // [Authorize]
        // public ActionResult EditComment(Guid id)
        // {
        //     SingleImageComment foundComment = this.commentsService.GetById(id);
        //     return this.View(foundCommentForView);
        // }

        // public ActionResult GetSelectedPageComments(int id, int pageNumber)
        // {
        //     var facilityComments = this.facilities.GetLatestFacilityComments(id);
        //     List<CommentViewModel> commentsViewModel = AutoMapperConfig.Configuration.CreateMapper().Map<List<CommentViewModel>>(facilityComments);
        //     CommentsListViewModel commentsListViewModel = new CommentsListViewModel();
        //     decimal totalCommentsCount = (decimal)commentsViewModel.Count();
        //     commentsListViewModel.TotalPages = (int)Math.Ceiling((totalCommentsCount / (decimal)SportsBook.Data.Common.Constants.Constants.COUNT_OF_COMMENTS_PER_PAGE));
        //     commentsListViewModel.CurrentPage = pageNumber;
        //     commentsListViewModel.Comments = commentsViewModel.Skip((int)(pageNumber - 1) * 5).Take(5).ToList();
        //     return this.PartialView("_PageableCommentsPartial", commentsListViewModel);
        // }
        // 
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // [Authorize]
        // public ActionResult EditComment(int id, CommentViewModel model)
        // {
        //     if (this.ModelState.IsValid)
        //     {
        //         FacilityComment foundComment = this.comments.GetById(id);
        //         this.comments.UpdateComment(id, model.Content);
        //         return this.RedirectToAction("FacilityDetails", "FacilitiesPublic", new { id = foundComment.FacilityId, area = "Facilities" });
        //     }
        // 
        //     return this.View(model);
        // }
        // 
        // [HttpPost]
        // [Authorize]
        // [ValidateAntiForgeryToken]
        // public ActionResult DeleteComment(int id)
        // {
        //     FacilityComment foundComment = this.comments.GetById(id);
        //     this.comments.DeleteComment(foundComment);
        //     return this.RedirectToAction("FacilityDetails", "FacilitiesPublic", new { id = foundComment.FacilityId, area = "Facilities" });
        // }
    }
}