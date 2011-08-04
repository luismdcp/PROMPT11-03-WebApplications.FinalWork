namespace Mod03_ChelasMovies.WebApp.Config
{
    public class AppConfiguration
    {
        public static int PageSize { get { return 3; } }
        public static string DefaultMoviesSortingCriteria { get { return "Title"; } }
        public static string OrderSegmentName { get { return "Order"; } }
        public static string SortingCriteriaSegmentName { get { return "SortingCriteria"; } }
    }
}