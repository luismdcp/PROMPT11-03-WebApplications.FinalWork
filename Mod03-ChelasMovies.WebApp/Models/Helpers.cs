using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Mod03_ChelasMovies.WebApp.Config;

namespace Mod03_ChelasMovies.WebApp.Models
{
    public static class Helpers
    {
        public static MvcHtmlString Image(this HtmlHelper helper, string url)
        {
            TagBuilder builder = new TagBuilder("img");
            builder.Attributes.Add("src", url);
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString SortingHeader(this HtmlHelper helper, string headerName)
        {
            var routeDictionary = new RouteValueDictionary();

            if (headerName != (string) helper.ViewContext.RouteData.Values[AppConfiguration.SortingCriteriaSegmentName])
            {
                routeDictionary[AppConfiguration.OrderSegmentName] = "Ascending";
            }
            else
            {
                routeDictionary[AppConfiguration.OrderSegmentName] = helper.ViewContext.RouteData.Values[AppConfiguration.OrderSegmentName].ToString().StartsWith("Asc") ? "Descending" : "Ascending";
            }

            routeDictionary[AppConfiguration.SortingCriteriaSegmentName] = headerName;
            return helper.RouteLink(headerName, "Paging", routeDictionary);
        }
    }
}