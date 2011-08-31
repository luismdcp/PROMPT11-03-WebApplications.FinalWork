using System.Collections.Generic;
using Mod03_ChelasMovies.DomainModel.Entities;
using Mod03_ChelasMovies.Rep;
using Mod03_ChelasMovies.Rep.Helpers.Collections;

namespace Mod03_ChelasMovies.DomainModel.ServicesRepositoryInterfaces
{
    public interface IMoviesRepository : IRepository<Movie, int>
    {
        Movie Search(string title);
        ICollection<Movie> GetMoviesCreatedByUser(string userName);
        IPagedList<Movie> GetAllReachable(string userName, string filterCriteria, int pageIndex, int pageSize, string sortingCriteria);
    }
}