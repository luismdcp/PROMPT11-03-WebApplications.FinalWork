
namespace Mod03_ChelasMovies.WebApp.Models.ViewModels
{
    /// <summary>
    /// View Model for the Index View of the Index Action from the Movies Controller
    /// </summary>
    public class SearchSortPagingViewModel
    {
        public SearchViewModel SearchModel { get; set; }
        public SortPagingViewModel SortAndPagingModel { get; set; }

        public SearchSortPagingViewModel(SearchViewModel searchModel, SortPagingViewModel sortAndPagingModel)
        {
            this.SearchModel = searchModel;
            this.SortAndPagingModel = sortAndPagingModel;
        }
    }
}