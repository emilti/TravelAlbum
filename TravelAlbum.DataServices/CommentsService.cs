using Bytes2you.Validation;
using System;
using System.Linq;
using TravelAlbum.Data;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Models;

namespace TravelAlbum.DataServices
{
    public class CommentsService : ICommentsService
    {
        private readonly IEfDbSetWrapper<SingleImageComment> commentsSetWrapper;

        private readonly ITravelAlbumEfDbContextSaveChanges travelAlbumEfDbContextSaveChanges;

        public CommentsService(IEfDbSetWrapper<SingleImageComment> commentsSetWrapper, ITravelAlbumEfDbContextSaveChanges travelAlbumEfDbContextSaveChanges)
        {
            Guard.WhenArgument(commentsSetWrapper, "commentsSetWrapper").IsNull().Throw();
            Guard.WhenArgument(travelAlbumEfDbContextSaveChanges, "travelAlbumEfDbContextSaveChanges").IsNull().Throw();

            this.commentsSetWrapper = commentsSetWrapper;
            this.travelAlbumEfDbContextSaveChanges = travelAlbumEfDbContextSaveChanges;
        }

        public void Add(SingleImageComment singleImageComment)
        { 
            this.commentsSetWrapper.Add(singleImageComment);
            this.travelAlbumEfDbContextSaveChanges.SaveChanges();
        }       

        public IQueryable<SingleImageComment> All()
        {
            return this.commentsSetWrapper.All;
        }

        public void DeleteComment(SingleImageComment singleImageComment)
        {
            this.commentsSetWrapper.Delete(singleImageComment);
            this.travelAlbumEfDbContextSaveChanges.SaveChanges();
        }

        public SingleImageComment GetById(Guid commentId)
        {
            SingleImageComment foundComment = this.commentsSetWrapper.GetById(commentId);
            return foundComment;
        }

        public void UpdateComment(Guid commentId, string newContent)
        {
            var comment = this.commentsSetWrapper.GetById(commentId);
            comment.Content = newContent;
            this.travelAlbumEfDbContextSaveChanges.SaveChanges();
        }
    }
}
