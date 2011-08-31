using Mod03_ChelasMovies.DomainModel.Entities;
using Mod03_ChelasMovies.Rep.Helpers.Collections;

namespace Mod03_ChelasMovies.WebApp.Models.ViewModels
{
    public class BaseViewModel
    {
        // Filtered Movies List by the User by filling the filter fields
        public IPagedList<Movie> SearchResults { get; set; }
    }
}