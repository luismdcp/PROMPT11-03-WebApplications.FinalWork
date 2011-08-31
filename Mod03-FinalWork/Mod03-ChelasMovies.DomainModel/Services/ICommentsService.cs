using System;
using System.Collections.Generic;
using Mod03_ChelasMovies.DomainModel.Entities;

namespace Mod03_ChelasMovies.DomainModel.Services
{
    public interface ICommentsService : IDisposable, IService<Comment, int>
    {
        ICollection<Comment> GetAllCommentsFromMovie(int movieId);
    }
}