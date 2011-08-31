using System;
using System.Collections.Generic;
using System.Linq;
using Mod03_ChelasMovies.DomainModel.Entities;
using Mod03_ChelasMovies.DomainModel.Services;
using Mod03_ChelasMovies.DomainModel.ServicesRepositoryInterfaces;

namespace Mod03_ChelasMovies.DomainModel.ServicesImpl
{
    public class RepositoryCommentsService : ICommentsService
    {
        #region Fields

        private readonly ICommentsRepository _commentsRepository;

        #endregion Fields

        #region Constructors

        public RepositoryCommentsService(ICommentsRepository commentsRepository)
        {
            this._commentsRepository = commentsRepository;
        }

        #endregion Constructors

        #region Public Methods

        public ICollection<Comment> GetAll()
        {
            return this._commentsRepository.GetAll().ToList();
        }

        public ICollection<Comment> GetAllCommentsFromMovie(int movieId)
        {
            return this._commentsRepository.GetAll().Where(c => c.MovieID == movieId).ToList();
        }

        public Comment Get(int id)
        {
            return this._commentsRepository.Get(id);
        }

        public void Add(Comment newComment)
        {
            this._commentsRepository.Add(newComment);
            this._commentsRepository.Save();
        }

        public void Update(Comment comment)
        {
            this._commentsRepository.Save();
        }

        public void Delete(int id)
        {
            try
            {
                this._commentsRepository.Delete(id);
                this._commentsRepository.Save();
            }
            catch (Exception e)
            {
                throw new ArgumentException(String.Format("Comment with id {0} could not be found", id), "id", e);
            }
        }

        public void Dispose()
        {
            this._commentsRepository.Dispose();
        }

        #endregion Public Methods
    }
}