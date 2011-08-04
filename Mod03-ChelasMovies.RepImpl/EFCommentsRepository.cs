using System.Collections.Generic;
using System.Linq;
using Mod03_ChelasMovies.DomainModel;
using Mod03_ChelasMovies.DomainModel.ServicesRepositoryInterfaces;
using Mod03_ChelasMovies.Rep;

namespace Mod03_ChelasMovies.RepImpl
{
    public class EFCommentsRepository : EFDbContextRepository<Comment, int>, ICommentsRepository
    {
        public EFCommentsRepository(MovieDbContext moviesContext) : base(moviesContext) { }

        ICollection<Comment> GetAllCommentsFromMovie(int movieId)
        {
            return ((MovieDbContext) this.DbContext).Comments.Where(c => c.MovieID == movieId).ToList();
        }
    }
}