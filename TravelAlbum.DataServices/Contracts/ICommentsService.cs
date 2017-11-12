using System;
using System.Linq;
using TravelAlbum.Models;

namespace TravelAlbum.DataServices.Contracts
{
    public interface ICommentsService
    {
        void Add(SingleImageComment singleImageComment);

        IQueryable<SingleImageComment> All();

        void DeleteComment(SingleImageComment singleImageComment);

        SingleImageComment GetById(Guid commentId);

        void UpdateComment(Guid commentId, string newContent);
    }
}
