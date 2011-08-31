using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Mod03_ChelasMovies.Tests.Utils;
using Mod03_ChelasMovies.WebApp;
using NUnit.Framework;

namespace Mod03_ChelasMovies.Tests.Controllers
{
    [TestFixture]
    public class UrlTests
    {
        #region Routes match

        #region Helper methods

        private static UrlHelper GetUrlHelper(string appPath = "/", RouteCollection routes = null)
        {
            if (routes == null)
            {
                routes = new RouteCollection();
                MvcApplication.RegisterRoutes(routes);
            }

            HttpContextBase httpContext = new TestHttpContext(appPath);
            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "defaultcontroller");
            routeData.Values.Add("action", "defaultaction");
            RequestContext requestContext = new RequestContext(httpContext, routeData);
            UrlHelper helper = new UrlHelper(requestContext, routes);
            return helper;
        }

        private static void TestRouteMatch(string httpVerb, string url, string controller, string action, object routeProperties = null)
        {
            // Arrange (obtain routing config + set up test context)
            RouteCollection routeCollection = new RouteCollection();
            MvcApplication.RegisterRoutes(routeCollection);
            HttpContextBase testContext = new TestHttpContext(url, httpVerb);

            // Act (run the routing engine against this HttpContextBase)
            RouteData routeData = routeCollection.GetRouteData(testContext);

            // Assert
            Assert.IsNotNull(routeData, "NULL RouteData was returned");
            Assert.IsNotNull(routeData.Route, "No route was matched");
            Assert.AreEqual(controller, routeData.Values["controller"], "Wrong controller");
            Assert.AreEqual(action, routeData.Values["action"], "Wrong action");

            if (routeProperties != null)
            {
                Assert.IsTrue(TestRouteValues(routeData.Values, routeProperties));
            }
        }

        private static bool TestRouteValues(RouteValueDictionary routeValues, object routeProperties)
        {
            bool result = true;
            PropertyInfo[] propInfo = routeProperties.GetType().GetProperties();

            foreach (PropertyInfo pi in propInfo)
            {
                if (!(routeValues.ContainsKey(pi.Name) && (Equals(routeValues[pi.Name], pi.GetValue(routeProperties, null).ToString()))))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        #endregion

        #region Comments URLs

        [Test]
        public void SlashCommentsSlashIndexGoesToGroupsControllerIndexActionByDefaultRoute()
        {
            TestRouteMatch("GET", "~/Comments/Index", "Comments", "Index");
        }

        [Test]
        public void SlashCommentsSlashCreateGoesToCommentsControllerCreateActionByDefaultRoute()
        {
            TestRouteMatch("GET", "~/Comments/Create", "Comments", "Create");
        }

        [Test]
        public void SlashCommentsSlashEditSlashIdGoesToCommentsControllerEditActionByDefaultRoute()
        {
            TestRouteMatch("GET", "~/Comments/Edit/5", "Comments", "Edit", new { id = 5 });
        }

        [Test]
        public void SlashCommentsSlashDeleteSlashIdGoesToCommentsControllerDeleteActionByDefaultRoute()
        {
            TestRouteMatch("GET", "~/Comments/Delete/5", "Comments", "Delete", new { id = 5 });
        }

        #endregion Comments URLs

        #region Groups URLs

        [Test]
        public void SlashGroupsSlashIndexGoesToGroupsControllerIndexActionByDefaultRoute()
        {
            TestRouteMatch("GET", "~/Groups/Index", "Groups", "Index");
        }

        [Test]
        public void SlashGroupsSlashDetailsSlashIdGoesToGroupsControllerDetailsActionByDefaultRoute()
        {
            TestRouteMatch("GET", "~/Groups/Details/1", "Groups", "Details", new { id = 1 });
        }

        [Test]
        public void SlashGroupsSlashCreateGoesToGroupsControllerCreateActionByDefaultRoute()
        {
            TestRouteMatch("GET", "~/Groups/Create", "Groups", "Create");
        }

        [Test]
        public void SlashGroupsSlashEditGoesToGroupssControllerEditActionByDefaultRoute()
        {
            TestRouteMatch("GET", "~/Groups/Edit/1", "Groups", "Edit", new { id = 1 });
        }

        [Test]
        public void SlashGroupsSlashEditGoesToGroupsControllerEditActionByDefaultRouteWithAPost()
        {
            TestRouteMatch("POST", "~/Groups/Edit/1", "Groups", "Edit", new { id = 1 });
        }

        [Test]
        public void SlashGroupsSlashDeletetGoesToGroupsControllerDeleteActionByDefaultRoute()
        {
            TestRouteMatch("GET", "~/Groups/Delete/1", "Groups", "Delete", new { id = 1 });
        }

        [Test]
        public void SlashGroupsSlashDeletetGoesToGroupsControllerDeleteActionByDefaultRouteWithAPost()
        {
            TestRouteMatch("POST", "~/Groups/Delete/1", "Groups", "Delete", new { id = 1 });
        }

        #endregion Groups URLs

        #region Movies URLs

        [Test]
        public void SlashGoesToMoviesControllerIndexActionByDefaultRoute()
        {
            TestRouteMatch("GET", "~/", "Movies", "Index");
        }

        [Test]
        public void SlashMoviesGoesToMoviesControllerIndexActionByDefaultRoute()
        {
            TestRouteMatch("GET", "~/Movies", "Movies", "Index");
        }

        [Test]
        public void SlashMoviesSlashIndexGoesToMoviesControllerIndexActionByDefaultRoute()
        {
            TestRouteMatch("GET", "~/Movies/Index", "Movies", "Index");
        }

        [Test]
        public void SlashMoviesSlashDetailsSlashIdGoesToMoviesControllerDetailsActionByDefaultRoute()
        {
            TestRouteMatch("GET", "~/Movies/Details/1", "Movies", "Details", new { id = 1 });
        }

        [Test]
        public void SlashMoviesSlashCreateGoesToMoviesControllerCreateActionByDefaultRoute()
        {
            TestRouteMatch("GET", "~/Movies/Create", "Movies", "Create");
        }

        [Test]
        public void SlashMoviesSlashEditGoesToMoviesControllerEditActionByDefaultRoute()
        {
            TestRouteMatch("GET", "~/Movies/Edit/1", "Movies", "Edit", new { id = 1 });
        }

        [Test]
        public void SlashMoviesSlashEditGoesToMoviesControllerEditActionByDefaultRouteWithAPost()
        {
            TestRouteMatch("POST", "~/Movies/Edit/1", "Movies", "Edit", new { id = 1 });
        }

        [Test]
        public void SlashMoviesSlashDeletetGoesToMoviesControllerDeleteActionByDefaultRoute()
        {
            TestRouteMatch("GET", "~/Movies/Delete/1", "Movies", "Delete", new { id = 1 });
        }

        [Test]
        public void SlashMoviesSlashDeletetGoesToMoviesControllerDeleteActionByDefaultRouteWithAPost()
        {
            TestRouteMatch("POST", "~/Movies/Delete/1", "Movies", "Delete", new { id = 1 });
        }

        [Test]
        public void SlashMoviesSlashPage1GoesToMoviesControllerIndexActionByDefaultRoute()
        {
            TestRouteMatch("GET", "~/Movies/Page1/Ascending/Title", "Movies", "Index", new { Page = 1, Order = "Ascending", SortingCriteria = "Title" });
        }

        #endregion Movies URLs

        #endregion Routes match

        #region Outbound Url generation with Routing

        [Test]
        public void ActionWithDefaultControllerAndOverrideAction()
        {
            // Arrange
            UrlHelper helper = GetUrlHelper();

            // Act
            string url = helper.Action("action");

            // Assert
            Assert.AreEqual("/defaultcontroller/action", url);
        }

        [Test]
        public void ActionWithOverrideControllerAndOverrideAction()
        {
            // Arrange
            UrlHelper helper = GetUrlHelper();

            // Act
            string url = helper.Action("action", "controller");

            // Assert
            Assert.AreEqual("/controller/action", url);
        }

        [Test]
        public void ActionWithOverrideControllerAndOverrideActionAndId()
        {
            // Arrange
            UrlHelper helper = GetUrlHelper();

            // Act
            string url = helper.Action("action", "controller", new { id = 42 });

            // Assert
            Assert.AreEqual("/controller/action/42", url);
        }

        [Test]
        public void RouteUrlWithDefaultControllerAndDefaultActiontValues()
        {
            // Arrange
            UrlHelper helper = GetUrlHelper();

            // Act
            string url = helper.RouteUrl(new { });

            // Assert
            Assert.AreEqual("/defaultcontroller/defaultaction", url);
        }

        [Test]
        public void RouteUrlWithNewValuesOverridesDefaultValues()
        {
            // Arrange
            UrlHelper helper = GetUrlHelper();

            // Act
            string url = helper.RouteUrl(new
            {
                controller = "controller",
                action = "action"
            });

            // Assert
            Assert.AreEqual("/controller/action", url);
        }

        [Test]
        public void RouteUrlWithPagingAndSortingValues()
        {
            // Arrange
            UrlHelper helper = GetUrlHelper();

            // Act
            string url = helper.RouteUrl(new
            {
                Page = "1",
                Order = "Ascending",
                SortingCriteria = "Title"
            });

            // Assert
            Assert.AreEqual("/defaultcontroller/Page1/Ascending/Title", url);
        }

        #endregion Outbound Url generation with Routing
    }
}