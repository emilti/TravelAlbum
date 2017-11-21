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
        private readonly IEfDbSetWrapper<Comment> commentsSetWrapper;

        private readonly ITravelAlbumEfDbContextSaveChanges travelAlbumEfDbContextSaveChanges;

        public CommentsService(IEfDbSetWrapper<Comment> commentsSetWrapper, ITravelAlbumEfDbContextSaveChanges travelAlbumEfDbContextSaveChanges)
        {
            Guard.WhenArgument(commentsSetWrapper, "commentsSetWrapper").IsNull().Throw();
            Guard.WhenArgument(travelAlbumEfDbContextSaveChanges, "travelAlbumEfDbContextSaveChanges").IsNull().Throw();

            this.commentsSetWrapper = commentsSetWrapper;
            this.travelAlbumEfDbContextSaveChanges = travelAlbumEfDbContextSaveChanges;
        }

        public void Add(Comment singleImageComment)
        { 
            this.commentsSetWrapper.Add(singleImageComment);
            this.travelAlbumEfDbContextSaveChanges.SaveChanges();
        }       

        public IQueryable<Comment> All()
        {
            return this.commentsSetWrapper.All;
        }

        public void DeleteComment(Comment singleImageComment)
        {
            this.commentsSetWrapper.Delete(singleImageComment);
            this.travelAlbumEfDbContextSaveChanges.SaveChanges();
        }

        public Comment GetById(Guid commentId)
        {
            Comment foundComment = this.commentsSetWrapper.GetById(commentId);
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
