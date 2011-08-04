namespace Mod03_ChelasMovies.DomainModel.ServicesRepositoryInterfaces {
    using System.Collections.Generic;
    using Rep;

    public interface IMoviesRepository : IRepository<Movie, int> {
        //Movie SearchByTitle(string title);
        //IQueryable<string> GetGenres();
        Movie Search(string title);
    }
}