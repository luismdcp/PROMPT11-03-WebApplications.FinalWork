using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using Mod03_ChelasMovies.DomainModel;
using Mod03_ChelasMovies.DomainModel.Entities;
using Mod03_ChelasMovies.DomainModel.ServicesRepositoryInterfaces;
using Mod03_ChelasMovies.Rep;
using Mod03_ChelasMovies.Rep.Helpers.Collections;

namespace Mod03_ChelasMovies.RepImpl
{
    public class EFIMDBAPIMoviesRepository : EFDbContextRepository<Movie, int>, IMoviesRepository
    {
        public EFIMDBAPIMoviesRepository(MovieDbContext moviesContext) : base(moviesContext) { }

        /// <summary>
        /// Gets the IMDB data for the movie title
        /// </summary>
        /// <param name="title">The movie title</param>
        /// <returns>Movie filled with the IMDB data</returns>
        public Movie Search(string title)
        {
            return TheIMDbAPI.SearchByTitle(title);
        }

        /// <summary>
        /// Gets all the Movies created by the logged User
        /// </summary>
        /// <param name="userName">The logged User name</param>
        /// <returns>Collection of Movies created by the logged User</returns>
        public ICollection<Movie> GetMoviesCreatedByUser(string userName)
        {
            return this.GetAll().Where(m => m.Owner.UserName == userName).ToList();
        }

        /// <summary>
        /// Gets all the reachable Movies by the logged User: Movies created by the User and Movies shared by other Users through Groups.
        /// </summary>
        /// <param name="userName">The logged User name</param>
        /// <param name="filterCriteria">string to pass to LINQ Dynamic for filtering results</param>
        /// <param name="pageIndex">The page Index</param>
        /// <param name="pageSize">THe number of Movies per page</param>
        /// <param name="sortingCriteria">The name of a Movie property to sort by</param>
        /// <returns>IPagedList with the Movies created by the User and Movies shared by other Users through Groups</returns>
        public IPagedList<Movie> GetAllReachable(string userName, string filterCriteria, int pageIndex, int pageSize, string sortingCriteria)
        {
            // Movies created by the User
            IEnumerable<Movie> createdMovies = this.GetMoviesCreatedByUser(userName).ToList();

            // Movies shared with the User through the Groups enrolled that were created by other Users
            IEnumerable<Movie> sharedMovies = this.DbContext.Set<Group>().Where(g => g.EnrolledUsers.Any(u => u.UserName == userName))
                                                            .SelectMany(gr => gr.SharedMovies).ToList();

            // All the Movies that the User is allowed to view and edit
            IQueryable<Movie> reachableMovies = createdMovies.Union(sharedMovies).AsQueryable();
            
            if (!String.IsNullOrEmpty(filterCriteria))
            {
                reachableMovies = reachableMovies.Where(filterCriteria);
            }

            if (!String.IsNullOrEmpty(sortingCriteria))
            {
                reachableMovies = reachableMovies.OrderBy(sortingCriteria);
            }

            return reachableMovies.ToPagedList(pageIndex, pageSize);
        }
    }
}