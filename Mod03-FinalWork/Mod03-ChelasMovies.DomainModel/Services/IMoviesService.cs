using System;
using System.Collections.Generic;
using Mod03_ChelasMovies.DomainModel.Entities;
using Mod03_ChelasMovies.Rep.Helpers.Collections;

namespace Mod03_ChelasMovies.DomainModel.Services
{
    public interface IMoviesService : IDisposable, IService<Movie, int>
    {
        IPagedList<Movie> GetAll(int pageIndex, int pageSize, string sortingCriteria);
        IPagedList<Movie> GetAll(string filteringCriteria, int pageindex, int pageSize, string sortingCriteria);
        ICollection<Movie> GetMoviesCreatedByUser(string userName);
        IPagedList<Movie> GetAllReachable(string userName, string filterCriteria, int pageIndex, int pageSize, string sortingCriteria);
        Movie Search(string title);
    }
}