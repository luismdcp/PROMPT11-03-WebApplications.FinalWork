using System;
using System.Collections.Generic;

namespace Mod03_ChelasMovies.DomainModel.Services
{
    public interface ICommentsService : IDisposable
    {
        ICollection<Comment> GetAllComments();
        ICollection<Comment> GetAllCommentsFromMovie(int movieId);
        Comment Get(int id);
        void Add(Comment newComment);
        void Update(Comment comment);
        void Delete(int id);
    }
}