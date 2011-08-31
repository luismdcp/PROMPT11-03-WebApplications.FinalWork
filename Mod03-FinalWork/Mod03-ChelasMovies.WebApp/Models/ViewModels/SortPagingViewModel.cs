using System;
using Mod03_ChelasMovies.WebApp.Config;

namespace Mod03_ChelasMovies.WebApp.Models.ViewModels
{
    /// <summary>
    /// View Model for sorting and paging the movies result list
    /// </summary>
    public class SortPagingViewModel : BaseViewModel
    {
        #region Properties

        public int Page { get; set; }
        public string Order { get; set; }

        public string SortingCriteriaWithOrder
        {
            get { return String.Format("{0} {1}", sortingCriteria ?? AppConfiguration.DefaultMoviesSortingCriteria, Order); }
            set { sortingCriteria = value; }
        }

        public string SortingCriteria
        {
            get { return sortingCriteria; }
            set { sortingCriteria = value; }
        }

        public int PageSize
        {
            get { return pageSize ?? AppConfiguration.PageSize; }
            set { pageSize = value; }
        }

        #endregion Properties

        #region Fields

        private string sortingCriteria;
        private int? pageSize;

        #endregion Fields
    }
}