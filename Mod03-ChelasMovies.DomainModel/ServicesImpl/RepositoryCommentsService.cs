using System;
using System.Collections.Generic;
using System.Linq;
using Mod03_ChelasMovies.DomainModel.Services;
using Mod03_ChelasMovies.DomainModel.ServicesRepositoryInterfaces;

namespace Mod03_ChelasMovies.DomainModel.ServicesImpl
{
    public class RepositoryCommentsService : ICommentsService
    {
        private readonly ICommentsRepository commentsRepository;

        public RepositoryCommentsService(ICommentsRepository commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        public ICollection<Comment> GetAllComments()
        {
            return this.commentsRepository.GetAll().ToList();
        }

        public ICollection<Comment> GetAllCommentsFromMovie(int movieId)
        {
            return this.commentsRepository.GetAll().Where(c => c.MovieID == movieId).ToList();
        }

        public Comment Get(int id)
        {
            return this.commentsRepository.Get(id);
        }

        public void Add(Comment newComment)
        {
            this.commentsRepository.Add(newComment);
            this.commentsRepository.Save();
        }

        public void Update(Comment comment)
        {
            this.commentsRepository.Save();
        }

        public void Delete(int id)
        {
            try
            {
                this.commentsRepository.Delete(id);
                this.commentsRepository.Save();
            }
            catch (Exception e)
            {
                throw new ArgumentException(String.Format("Comment with id {0} could not be found", id), "id", e);
            }
        }

        public void Dispose()
        {
            this.commentsRepository.Dispose();
        }
    }
}