using System;
using Mod03_ChelasMovies.DomainModel;
using Mod03_ChelasMovies.Rep.Helpers.Collections;
using Mod03_ChelasMovies.WebApp.Config;

namespace Mod03_ChelasMovies.WebApp.Models.ViewModels
{
    public class SearchViewModel
    {
        public int Page { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public IPagedList<Movie> SearchResults { get; set; }
        public string SearchButton { get; set; }
        public string Order { get; set; }

        private string sortingCriteria;
        private int? pageSize;

        public string SortingCriteria
        {
            get { return String.Format("{0} {1}", sortingCriteria ?? AppConfiguration.DefaultMoviesSortingCriteria, Order); }
            set { sortingCriteria = value; }
        }

        public int PageSize
        {
            get { return pageSize ?? AppConfiguration.PageSize; }
            set { pageSize = value; }
        }
    }
}