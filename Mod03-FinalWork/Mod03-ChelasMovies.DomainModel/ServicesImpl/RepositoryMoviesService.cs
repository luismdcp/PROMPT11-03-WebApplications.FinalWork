using System;
using System.Collections.Generic;
using System.Linq;
using Mod03_ChelasMovies.DomainModel.Entities;
using Mod03_ChelasMovies.DomainModel.Services;
using Mod03_ChelasMovies.DomainModel.ServicesRepositoryInterfaces;
using Mod03_ChelasMovies.Rep.Helpers.Collections;

namespace Mod03_ChelasMovies.DomainModel.ServicesImpl
{
    public class RepositoryMoviesService : IMoviesService
    {
        #region Fields

        private readonly IMoviesRepository _moviesRepository;

        #endregion Fields

        #region Constructors

        public RepositoryMoviesService(IMoviesRepository moviesRepository)
        {
            this._moviesRepository = moviesRepository;
        }

        #endregion Constructors

        #region Public Methods

        public Movie Get(int id)
        {
            return _moviesRepository.Get(id);
        }

        public void Add(Movie newMovie)
        {
            _moviesRepository.Add(newMovie);
            _moviesRepository.Save();
        }

        public void Update(Movie movie)
        {
            _moviesRepository.Save();
        }

        public void Delete(int id)
        {
            try
            {
                _moviesRepository.Delete(id);
                _moviesRepository.Save();
            }
            catch (Exception e)
            {
                throw new ArgumentException(String.Format("Movie with id {0} could not be found", id), "id", e);
            }
        }

        public Movie Search(string title)
        {
            return _moviesRepository.Search(title);
        }

        public void Dispose()
        {
            _moviesRepository.Dispose();
        }

        public IPagedList<Movie> GetAll(int pageIndex, int pageSize, string sortingCriteria)
        {
            return this._moviesRepository.GetAll(pageIndex, pageSize, sortingCriteria);
        }

        public IPagedList<Movie> GetAll(string filteringCriteria, int pageIndex, int pageSize, string sortingCriteria)
        {
            return this._moviesRepository.GetAll(filteringCriteria, pageIndex, pageSize, sortingCriteria);
        }

        public IPagedList<Movie> GetAllReachable(string userName, string filterCriteria, int pageIndex, int pageSize, string sortingCriteria)
        {
            return this._moviesRepository.GetAllReachable(userName, filterCriteria, pageIndex, pageSize, sortingCriteria);
        }

        public ICollection<Movie> GetAll()
        {
            return this._moviesRepository.GetAll().ToList();
        }

        public ICollection<Movie> GetMoviesCreatedByUser(string userName)
        {
            return this._moviesRepository.GetMoviesCreatedByUser(userName);
        }

        #endregion Public Methods
    }
}