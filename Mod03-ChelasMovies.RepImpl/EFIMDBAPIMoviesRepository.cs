using Mod03_ChelasMovies.Rep;
using System.Linq;

namespace Mod03_ChelasMovies.RepImpl
{
    using DomainModel;
    using DomainModel.ServicesRepositoryInterfaces;

    public class EFIMDBAPIMoviesRepository : EFDbContextRepository<Movie, int>, IMoviesRepository
    {
        public EFIMDBAPIMoviesRepository(MovieDbContext moviesContext) : base(moviesContext) { }

        public Movie GetWithComments(int id)
        {
            return ((MovieDbContext) this.DbContext).Movies.Include("Comments").Where(m => m.ID == id).FirstOrDefault();
        }

        public Movie Search(string title)
        {
            return TheIMDbAPI.SearchByTitle(title);
        }
    }
}