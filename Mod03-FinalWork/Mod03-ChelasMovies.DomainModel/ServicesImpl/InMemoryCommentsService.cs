using System.Collections.Generic;
using System.Linq;
using Mod03_ChelasMovies.DomainModel.Entities;
using Mod03_ChelasMovies.DomainModel.Services;
using System.Threading;

namespace Mod03_ChelasMovies.DomainModel.ServicesImpl
{
    public class InMemoryCommentsService : ICommentsService
    {
        #region Fields

        static List<Movie> _movies;
        static List<Comment> _comments;
        static int _newId;

        #endregion Fields

        #region Constructors

        static InMemoryCommentsService()
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
                                           ID = 1,
                                           Description = "Description 1",
                                           Rating = 3,
                                           Movie = _movies[0]
                                       },
                                   new Comment
                                       {
                                           ID = 2,
                                           Description = "Description 2",
                                           Rating = 4,
                                           Movie = _movies[0]
                                       },

                                   new Comment
                                       {
                                           ID = 3,
                                           Description = "Description 3",
                                           Rating = 5,
                                           Movie = _movies[1]
                                       },
                                       new Comment
                                       {
                                           ID = 4,
                                           Description = "Description 4",
                                           Rating = 4,
                                           Movie = _movies[1]
                                       },

                               };

            _newId = _comments.Count;
        }

        #endregion Constructors

        #region Public Methods

        public ICollection<Comment> GetAll()
        {
            return _comments;
        }

        public ICollection<Comment> GetAllCommentsFromMovie(int movieId)
        {
            return _comments.Where(c => c.Movie.ID == movieId).ToList();
        }

        public Comment Get(int id)
        {
            return _comments.FirstOrDefault(c => c.ID == id);
        }

        public void Add(Comment newComment)
        {
            newComment.ID = Interlocked.Increment(ref _newId);
            _comments.Add(newComment);
        }

        public void Update(Comment comment)
        {
            
        }

        public void Delete(int id)
        {
            Comment commentToDelete = this.Get(id);
            _comments.Remove(commentToDelete);
        }

        public void Dispose()
        {
            _movies.Clear();
            _comments.Clear();
        }

        #endregion Public Methods
    }
}