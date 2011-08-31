using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Mod03_ChelasMovies.DomainModel.Entities;
using Mod03_ChelasMovies.DomainModel.Services;
using Mod03_ChelasMovies.Rep.Helpers.Collections;

namespace Mod03_ChelasMovies.DomainModel.ServicesImpl
{
    public class InMemoryMoviesService : IMoviesService
    {
        #region Fields

        static List<Movie> _movies;
        static List<Comment> _comments;
        static int _newId;

        #endregion Fields

        #region Constructors

        static InMemoryMoviesService()
        {

            _movies = new List<Movie>
                             {

                                 new Movie
                                     {
                                         ID = 1,
                                         Title = "When Harry Met Sally1",
                                         Genre = "Romantic Comedy",
                                         Year = 2004,
                                         Comments = new List<Comment>(),
                                         Image = "~/Content/logo.png"
                                     },

                                 new Movie
                                     {
                                         ID = 2,
                                         Title = "Ghostbusters 2",
                                         Genre = "Comedy",
                                         Year = 2002,
                                         Comments = new List<Comment>(),
                                         Image = "~/Content/logo.png"
                                     },
                                 new Movie
                                     {
                                         ID = 3,
                                         Title = "Ninja das Caldas",
                                         Genre = "Comedy",
                                         Year = 2000,
                                         Comments = new List<Comment>(),
                                         Image = "~/Content/logo.png"
                                     },
                             };

            _comments = new List<Comment>
                               {
                                   new Comment
                                       {
                                           Description = "Description 1",
                                           Rating = 3,
                                           Movie = _movies[0]
                                       },
                                   new Comment
                                       {
                                           Description = "Description 2",
                                           Rating = 4,
                                           Movie = _movies[0]
                                       },

                                   new Comment
                                       {
                                           Description = "Description 3",
                                           Rating = 5,
                                           Movie = _movies[1]
                                       },
                                       new Comment
                                       {
                                           Description = "Description 4",
                                           Rating = 4,
                                           Movie = _movies[1]
                                       },

                               };

            _newId = _movies.Count;
        }

        #endregion Constructors

        #region Public Methods

        public ICollection<Movie> GetAllMovies()
        {
            return _movies;
        }

        public Movie Get(int id)
        {
            return _movies.FirstOrDefault(m => m.ID == id);
        }

        public Movie GetWithComments(int id)
        {
            return Get(id);
        }

        public void Add(Movie newMovie)
        {
            newMovie.ID = Interlocked.Increment(ref _newId);
            _movies.Add(newMovie);
        }

        public void Update(Movie movie)
        {
            
        }

        public void Delete(int id)
        {
            Movie movieToDelete = this.Get(id);
            _movies.Remove(movieToDelete);
        }

        public Movie Search(string title)
        {
            return null;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IPagedList<Movie> GetAll(int pageIndex, int pageSize, string sortingCriteria)
        {
            throw new NotImplementedException();
        }

        public IPagedList<Movie> GetAll(string filteringCriteria, int pageIndex, int pageSize, string sortingCriteria)
        {
            throw new NotImplementedException();
        }

        public ICollection<Movie> GetAll()
        {
            return _movies;
        }

        public ICollection<Movie> GetMoviesCreatedByUser(string userName)
        {
            return _movies.Where(m => m.Owner.UserName == userName).ToList();
        }

        public IPagedList<Movie> GetAll(string userName, string filteringCriteria, int pageIndex, int pageSize, string sortingCriteria)
        {
            throw new NotImplementedException();
        }

        public IPagedList<Movie> GetAllReachable(string userName, string filterCriteria, int pageIndex, int pageSize, string sortingCriteria)
        {
            return this.GetAll().ToPagedList(pageIndex, pageSize);
        }

        #endregion Public Methods
    }
}