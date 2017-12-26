using System;
using System.Linq;
using TravelAlbum.Models;

namespace TravelAlbum.DataServices.Contracts
{
    public interface ICommentsService
    {
        void Add(Comment imageComment);

        IQueryable<Comment> All();

        void DeleteComment(Comment imageComment);

        Comment GetById(Guid commentId);

        void UpdateComment(Guid commentId, string newContent);
    }
}
