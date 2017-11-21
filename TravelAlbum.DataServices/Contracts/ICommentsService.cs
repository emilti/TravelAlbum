using System;
using System.Linq;
using TravelAlbum.Models;

namespace TravelAlbum.DataServices.Contracts
{
    public interface ICommentsService
    {
        void Add(Comment singleImageComment);

        IQueryable<Comment> All();

        void DeleteComment(Comment singleImageComment);

        Comment GetById(Guid commentId);

        void UpdateComment(Guid commentId, string newContent);
    }
}
