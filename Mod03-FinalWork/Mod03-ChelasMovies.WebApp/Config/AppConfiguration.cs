namespace Mod03_ChelasMovies.WebApp.Config
{
    public static class AppConfiguration
    {
        public static int PageSize { get { return 3; } } // Default page size for paging
        public static string DefaultMoviesSortingCriteria { get { return "Title"; } } // default column name for the firt sort
        public static string OrderSegmentName { get { return "Order"; } } // Name of the Order segment in the Paging Route
        public static string SortingCriteriaSegmentName { get { return "SortingCriteria"; } } // Name of the SortingCriteria segment in the Paging Route
    }
}