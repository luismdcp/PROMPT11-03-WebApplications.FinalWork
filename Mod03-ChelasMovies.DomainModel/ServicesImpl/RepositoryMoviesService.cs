using System;
using System.Collections.Generic;
using System.Linq;
using Mod03_ChelasMovies.DomainModel.Services;
using Mod03_ChelasMovies.DomainModel.ServicesRepositoryInterfaces;
using Mod03_ChelasMovies.Rep.Helpers.Collections;

namespace Mod03_ChelasMovies.DomainModel.ServicesImpl
{
    public class RepositoryMoviesService : IMoviesService
    {
        private readonly IMoviesRepository moviesRepository;

        public RepositoryMoviesService(IMoviesRepository moviesRepository)
        {
            this.moviesRepository = moviesRepository;
        }

        public ICollection<Movie> GetAllMovies()
        {
            return moviesRepository.GetAll().ToList();
        }

        public Movie Get(int id)
        {
            return moviesRepository.Get(id);
        }

        public Movie GetWithComments(int id)
        {
            return moviesRepository.Get(id);
        }

        public void Add(Movie newMovie)
        {
            moviesRepository.Add(newMovie);
            moviesRepository.Save();
        }

        public void Update(Movie movie)
        {
            moviesRepository.Save();
        }

        public void Delete(int id)
        {
            try
            {
                moviesRepository.Delete(id);
                moviesRepository.Save();
            }
            catch (Exception e)
            {
                throw new ArgumentException(String.Format("Movie with id {0} could not be found", id), "id", e);
            }
        }

        public Movie Search(string title)
        {
            return moviesRepository.Search(title);
        }

        public void Dispose()
        {
            moviesRepository.Dispose();
        }


        public IPagedList<Movie> GetAll(int pageIndex, int pageSize, string sortingCriteria)
        {
            return this.moviesRepository.GetAll(pageIndex, pageSize, sortingCriteria);
        }

        public IPagedList<Movie> GetAll(string filteringCriteria, int pageIndex, int pageSize, string sortingCriteria)
        {
            return this.moviesRepository.GetAll(pageIndex, pageSize, sortingCriteria);
        }
    }
}