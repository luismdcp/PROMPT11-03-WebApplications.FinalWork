using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;
using Mod03_ChelasMovies.DependencyResolution;
using Mod03_ChelasMovies.DomainModel;
using Mod03_ChelasMovies.WebApp.Models;

namespace Mod03_ChelasMovies.WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Paging", // Route name
                "{controller}/Page{Page}/{Order}/{SortingCriteria}", // URL with parameters
                new { controller = "Movies", action = "Index", Page = 0,  Order = UrlParameter.Optional, SortingCriteria = UrlParameter.Optional }
               ,new { Order = "Asc|Desc|Ascending|Descending", Page = @"\d+" }// Parameter defaults
            );

            routes.MapRoute(
                "Comments", // Route name
                "Movies/{movieId}/{action}/{commentId}", // URL with parameters
                new { controller = "Movies", commentId = UrlParameter.Optional }, // Parameter defaults,
                new { movieId = @"\d+" }
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Movies", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            Database.SetInitializer(new MoviesInitializer());
            AppStart_Structuremap.Start();
            Database.SetInitializer<MovieDbContext>(new MoviesInitializer());

            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}