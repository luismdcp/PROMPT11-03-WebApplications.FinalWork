// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HtmlHelperEnhancer.cs" company="Servisys Solutions, Lda - Quickipic">
//   Luís Falcão - 2009
// </copyright>
// <summary>
//  Extension methods to <see cref="HtmlHelper"/> to support sortable columns in grids, sorting
//  and other utility methods.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Mod03_ChelasMovies.WebApp.Config;

namespace Mod03_ChelasMovies.WebApp.Helpers
{
    /// <summary>
    /// Extension methods to <see cref="HtmlHelper"/> to support sortable columns in grids, sorting and other utility methods.
    /// </summary>
    public static class HtmlHelperEnhancer
    {
        public static MvcHtmlString DropDownListForEnum<TEnum>(this HtmlHelper helper, string name, object selectedItemValue) where TEnum : struct
        {
            return helper.DropDownListForEnum<TEnum>(name, selectedItemValue, null);
        }

        public static MvcHtmlString DropDownListForEnum<TEnum>(this HtmlHelper helper, string name, object selectedItemValue, object htmlAttributes) where TEnum : struct
        {
            return helper.DropDownListForEnum(typeof(TEnum), name, selectedItemValue, htmlAttributes);
        }

        public static MvcHtmlString DropDownListForEnum(this HtmlHelper helper, Type enumType, string name, object selectedItemValue)
        {
            return helper.DropDownListForEnum(enumType, name, selectedItemValue, null);
        }

        public static MvcHtmlString DropDownListForEnum(this HtmlHelper helper, Type enumType, string name, object selectedItemValue, object htmlAttributes)
        {
            IEnumerable<SelectListItem> itens = Enum.GetValues(enumType).Cast<object>().Select(v =>
            {
                int value = (int) v;

                return new SelectListItem
                {
                    Value = value.ToString(),
                    Text = v.ToString(),
                    Selected = v.Equals(selectedItemValue)
                };
            });

            return helper.DropDownList(name, itens);//, htmlAttributes);
        }

        /// <summary>
        /// Generates a img html element from a URL
        /// </summary>
        /// <param name="helper">HtmlHelper for the extension method</param>
        /// <param name="url">The URL for the src atribute</param>
        /// <returns></returns>
        public static MvcHtmlString Image(this HtmlHelper helper, string url)
        {
            TagBuilder builder = new TagBuilder("img");
            builder.Attributes.Add("src", url);
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }

        /// <summary>
        /// Generates a clickable column name that toggles de sorting order
        /// </summary>
        /// <param name="helper">HtmlHelper for the extension method</param>
        /// <param name="headerName">The Column name</param>
        /// <returns></returns>
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

        /// <summary>
        /// Generates a clickable image (an up or down arrow) that alters the sorting order
        /// </summary>
        /// <param name="helper">HtmlHelper for the extension method</param>
        /// <param name="imageUrl">The image URL for the src atribute</param>
        /// <param name="routeValues">Values to be used by the Paging Route</param>
        /// <returns></returns>
        public static MvcHtmlString ImageLink(this HtmlHelper helper, string imageUrl, object routeValues)
        {
            UrlHelper urlHelper = ((Controller) helper.ViewContext.Controller).Url;
            TagBuilder imageBuilder = new TagBuilder("img");
            imageBuilder.Attributes.Add("src", imageUrl);

            string url = urlHelper.RouteUrl("Paging", routeValues);

            TagBuilder imageLink = new TagBuilder("a");
            imageLink.MergeAttribute("href", url);
            imageLink.InnerHtml = imageBuilder.ToString();

            return MvcHtmlString.Create(imageLink.ToString());
        }
    }
}