
namespace Mod03_ChelasMovies.WebApp.Models.ViewModels
{
    /// <summary>
    /// View Model for the Shared Movies block
    /// </summary>
    public class MoviesDetailsViewModel
    {
        public string AddMoviesButton { get; set; } // To check if the user clicked the Add Button from the Shared Movies block
        public string DeleteMoviesButton { get; set; } // To check if the user clicked the Delete Button from the Shared Movies block
        public int[] DeleteSharedMovies { get; set; } // Collection of Movies IDs that were selected from the checkbox list in the Shared Movies block for deletion
    }
}