using System;
using System.Collections.Generic;
using System.Linq;
using Mod03_ChelasMovies.DomainModel.Services;

namespace Mod03_ChelasMovies.DomainModel.ServicesImpl
{
    public class EFMoviesService : IMoviesService, IDisposable
    {
        private readonly MovieDbContext _movieDbContext;

        public EFMoviesService(MovieDbContext movieDbContext)
        {
            _movieDbContext = movieDbContext;
        }

        public ICollection<Movie> GetAllMovies()
        {
            return _movieDbContext.Movies.ToList();
        }

        public Movie Get(int id)
        {
            return _movieDbContext.Movies.Find(id);
        }

        public Movie GetWithComments(int id)
        {
            return _movieDbContext.Movies.Find(id);
        }

        public void Add(Movie newMovie)
        {
            _movieDbContext.Movies.Add(newMovie);
            _movieDbContext.SaveChanges();
        }

        public void Update(Movie movie)
        {
            _movieDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            Movie m = _movieDbContext.Movies.Find(id);
            if(m == null)
            {
                throw new ArgumentException(String.Format("Movie with id {0} could not be found", id), "id");
            }

            _movieDbContext.Movies.Remove(m);
            _movieDbContext.SaveChanges();
        }

        public Movie Search(string title)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _movieDbContext.Dispose();
        }


        public Rep.Helpers.Collections.IPagedList<Movie> GetAll(int pageIndex, int pageSize, string sortingCriteria)
        {
            throw new NotImplementedException();
        }

        public Rep.Helpers.Collections.IPagedList<Movie> GetAll(string filteringCriteria, int pageindex, int pageSize, string sortingCriteria)
        {
            throw new NotImplementedException();
        }
    }
}