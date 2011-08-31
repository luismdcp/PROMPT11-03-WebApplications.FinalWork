using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Mod03_ChelasMovies.DomainModel.Entities;
using Mod03_ChelasMovies.DomainModel.Services;
using Mod03_ChelasMovies.DomainModel.ServicesImpl;
using Mod03_ChelasMovies.Tests.Utils;
using Mod03_ChelasMovies.WebApp.Controllers;
using Mod03_ChelasMovies.WebApp.Models.ViewModels;
using NUnit.Framework;

namespace Mod03_ChelasMovies.Tests.Controllers
{
    [TestFixture]
    public class MoviesControllerTests
    {
        #region Fields

        private IMoviesService _inMemoryMoviesService;
        private IGroupsService _inMemoryGroupsService;
        private MoviesController _controller;

        #endregion Fields

        #region Setup

        [SetUp]
        public void SetUp()
        {
            this._inMemoryMoviesService = new InMemoryMoviesService();
            this._inMemoryGroupsService = new InMemoryGroupsService();
            this._controller = new MoviesController(_inMemoryMoviesService, _inMemoryGroupsService);
        }

        #endregion Setup

        #region Tests when Index action executes

        [Test]
        public void ShouldRenderAllExistingMoviesWithoutSearchAndPaging()
        {
            // Arrange
            SearchViewModel searchModel = new SearchViewModel();
            SortPagingViewModel sortPagingModel = new SortPagingViewModel {Page = 0, PageSize = 4};
            IPrincipal fakeUser = new GenericPrincipal(new GenericIdentity("User1", "Forms"), null);
            var moviesCount = this._inMemoryMoviesService.GetAll().Count;

            // Act
            ActionResult actual = _controller.Index(searchModel, sortPagingModel, fakeUser);

            // Assert
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<ViewResult>(actual, "The returned ActionResult was not an instance of ViewResult");

            ViewResult viewResult = (ViewResult) actual;
            Assert.IsNotNull(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is Null");
            Assert.IsInstanceOf<SearchSortPagingViewModel>(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is not an instance of SearchSortPagingViewModel");

            SearchSortPagingViewModel model = (SearchSortPagingViewModel) viewResult.ViewData.Model;
            Assert.IsTrue(model.SearchModel.SearchResults.Count == moviesCount, string.Format("The value of Model.SearchModel.SearchResults.Count should be '{0}' but it is '{1}'", moviesCount, model.SearchModel.SearchResults.Count));
        }

        #endregion Tests when Index action executes

        #region Tests when Details action executes

        [Test]
        public void ShouldRenderTheDetailsForAnExistingMovie()
        {
            // Arrange
            const int id = 1;
            Movie movie = _inMemoryMoviesService.Get(id);

            // Act
            ActionResult actual = _controller.Details(id);

            // Assert
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<ViewResult>(actual, "The returned ActionResult was not an instance of ViewResult");

            ViewResult viewResult = (ViewResult) actual;
            Assert.IsNotNull(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is Null");
            Assert.IsInstanceOf<Movie>(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is not an instance of Movie");

            Movie model = (Movie) viewResult.ViewData.Model;
            Assert.IsTrue(model.ID == movie.ID, string.Format("The value of Model.ID should be '{0}' but it is '{1}'", movie.ID, model.ID));
            Assert.IsTrue(model.Director == movie.Director, string.Format("The value of Model.Director should be '{0}' but it is '{1}'", movie.Director, model.Director));
            Assert.IsTrue(model.Actors == movie.Actors, string.Format("The value of Model.Actors should be '{0}' but it is '{1}'", movie.Actors, model.Actors));
            Assert.IsTrue(model.Genre == movie.Genre, string.Format("The value of Model.Genre should be '{0}' but it is '{1}'", movie.Genre, model.Genre));
            Assert.IsTrue(model.Image == movie.Image, string.Format("The value of Model.Image should be '{0}' but it is '{1}'", movie.Image, model.Image));
            Assert.IsTrue(model.Runtime == movie.Runtime, string.Format("The value of Model.Runtime should be '{0}' but it is '{1}'", movie.Runtime, model.Runtime));
            Assert.IsTrue(model.Title == movie.Title, string.Format("The value of Model.Title should be '{0}' but it is '{1}'", movie.Title, model.Title));
            Assert.IsTrue(model.Year == movie.Year, string.Format("The value of Model.Year should be '{0}' but it is '{1}'", movie.Year, model.Year));
        }

        [Test]
        public void ShouldRenderTheNotFoundViewForANonExistingMovie()
        {
            // Arrange
            const int id = 10;

            // Act
            ActionResult actual = _controller.Details(id);

            // Assert
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<ViewResult>(actual, "The returned ActionResult was not an instance of ViewResult");

            ViewResult viewResult = (ViewResult) actual;
            Assert.IsNotNull(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is Null");
            Assert.IsInstanceOf<NotFoundViewModel>(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is not an instance of NotFoundViewModel");

            NotFoundViewModel model = (NotFoundViewModel) viewResult.ViewData.Model;
            Assert.IsTrue(model.Id == id, string.Format("The value of Model.Id should be '{0}' but it is '{1}'", id, model.Id));
            Assert.IsTrue(model.Entity == "Movie", string.Format("The value of Model.Entity should be 'Movie' but it is '{0}'", model.Entity));
        }

        #endregion

        #region Tests when Create actions execute

        [Test]
        public void ShouldRenderCreateViewByGETForANewMovie()
        {
            // Act
            ActionResult actual = _controller.Create();

            // Assert
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<ViewResult>(actual, "The returned ActionResult was not an instance of ViewResult");

            ViewResult viewResult = (ViewResult) actual;
            Assert.IsNotNull(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is Null");
            Assert.IsInstanceOf<Movie>(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is not an instance of Comment");

            Movie model = (Movie) viewResult.ViewData.Model;
            Assert.IsTrue(model.ID == 0, string.Format("The value of Model.ID should be '{0}' but it is '{1}'", 0, model.ID));
        }

        [Test]
        public void ShouldRenderMovieDetailsViewByPOSTForANewValidMovie()
        {
            // Arrange
            HttpContextBase testContext = new TestHttpContext("~/Movies/Create", "POST");
            ControllerContext context = new ControllerContext(new RequestContext(testContext, new RouteData()), _controller);
            _controller.ControllerContext = context;

            var fakeForm = new FormCollection { { "Year", "2011" }, { "Title", "When Harry Met Sally1" }};
            _controller.ValueProvider = fakeForm.ToValueProvider();
            IPrincipal fakeUser = new GenericPrincipal(new GenericIdentity("User1", "Forms"), null);

            // Act
            ActionResult actual = _controller.Create(string.Empty, string.Empty, fakeUser);

            // Assert
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<RedirectToRouteResult>(actual, "The returned ActionResult was not an instance of RedirectToRouteResult");

            RedirectToRouteResult viewResult = (RedirectToRouteResult) actual;
            Assert.IsTrue(viewResult.RouteValues["Action"].ToString() == "Details", string.Format("The value of RouteValues[\"Action\"] should be 'Details' but it is '{0}'", viewResult.RouteValues["Action"]));
            Assert.IsTrue((int) viewResult.RouteValues["id"] == 4, string.Format("The RouteValues[\"id\"] should be '{0}' but it is '{1}'", 4, viewResult.RouteValues["id"]));
        }

        [Test]
        public void ShouldRenderCreateViewByPOSTForAnInvalidMovie()
        {
            // Arrange
            HttpContextBase testContext = new TestHttpContext("~/Movies/Create", "POST");
            ControllerContext context = new ControllerContext(new RequestContext(testContext, new RouteData()), _controller);
            _controller.ControllerContext = context;

            var fakeForm = new FormCollection { { "Year", "2011" }, { "Title", "When Harry Met Sally1" } };
            _controller.ValueProvider = fakeForm.ToValueProvider();
            _controller.ViewData.ModelState.AddModelError("Key", "ErrorMessage");
            IPrincipal fakeUser = new GenericPrincipal(new GenericIdentity("User1", "Forms"), null);

            // Act
            ActionResult actual = _controller.Create(string.Empty, string.Empty, fakeUser);

            // Assert
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<ViewResult>(actual, "The returned ActionResult was not an instance of ViewResult");

            ViewResult viewResult = (ViewResult) actual;
            Assert.IsNotNull(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is Null");
            Assert.IsInstanceOf<Movie>(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is not an instance of Movie");

            Movie model = (Movie) viewResult.ViewData.Model;
            Assert.IsTrue(model.ID == 0, string.Format("The value of Model.MovieID should be '{0}' but it is '{1}'", 0, model.ID));
            Assert.IsTrue(model.Year == 2011, string.Format("The value of Model.Year should be '{0}' but it is '{1}'", 2011, model.Year));
            Assert.IsTrue(model.Title == "When Harry Met Sally1", string.Format("The value of Model.Title should be '{0}' but it is '{1}'", "When Harry Met Sally1", model.Title));
        }

        #endregion

        #region Tests when Edit actions execute

        [Test]
        public void ShouldRenderEditViewByGETForAExistingMovie()
        {
            // Arrange
            const int id = 1;
            Movie movie = _inMemoryMoviesService.Get(id);

            // Act
            ActionResult actual = _controller.Edit(id);

            // Assert
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<ViewResult>(actual, "The returned ActionResult was not an instance of ViewResult");

            ViewResult viewResult = (ViewResult) actual;
            Assert.IsNotNull(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is Null");
            Assert.IsInstanceOf<Movie>(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is not an instance of Comment");

            Movie model = (Movie) viewResult.ViewData.Model;
            Assert.IsTrue(model.ID == movie.ID, string.Format("The value of Model.ID should be '{0}' but it is '{1}'", movie.ID, model.ID));
            Assert.IsTrue(model.Director == movie.Director, string.Format("The value of Model.Director should be '{0}' but it is '{1}'", movie.Director, model.Director));
            Assert.IsTrue(model.Actors == movie.Actors, string.Format("The value of Model.Actors should be '{0}' but it is '{1}'", movie.Actors, model.Actors));
            Assert.IsTrue(model.Genre == movie.Genre, string.Format("The value of Model.Genre should be '{0}' but it is '{1}'", movie.Genre, model.Genre));
            Assert.IsTrue(model.Image == movie.Image, string.Format("The value of Model.Image should be '{0}' but it is '{1}'", movie.Image, model.Image));
            Assert.IsTrue(model.Runtime == movie.Runtime, string.Format("The value of Model.Runtime should be '{0}' but it is '{1}'", movie.Runtime, model.Runtime));
            Assert.IsTrue(model.Title == movie.Title, string.Format("The value of Model.Title should be '{0}' but it is '{1}'", movie.Title, model.Title));
            Assert.IsTrue(model.Year == movie.Year, string.Format("The value of Model.Year should be '{0}' but it is '{1}'", movie.Year, model.Year));
        }

        [Test]
        public void ShouldRenderEditViewByPOSTForANewUpdatedAndValidMovie()
        {
            // Arrange
            const int id = 2;

            HttpContextBase testContext = new TestHttpContext("~/Movies/Edit/1", "POST");
            ControllerContext context = new ControllerContext(new RequestContext(testContext, new RouteData()), _controller);
            _controller.ControllerContext = context;

            var fakeForm = new FormCollection { { "ID", "1" }, { "Title", "When Harry Met Sally1" }, { "Genre", "Chic movie" }, { "Year", "2004" }, { "Image", "~/Content/logo.png" } };
            _controller.ValueProvider = fakeForm.ToValueProvider();

            // Act
            ActionResult actual = _controller.EditPost(id);

            // Assert
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<RedirectToRouteResult>(actual, "The returned ActionResult was not an instance of RedirectToRouteResult");

            RedirectToRouteResult viewResult = (RedirectToRouteResult) actual;
            Assert.IsNotNull(viewResult.RouteName == "Default", string.Format("The RedirectToRouteResult RouteName should be 'Default' but it is '{0}'", viewResult.RouteName));
            Assert.IsTrue(viewResult.RouteValues["Action"].ToString() == "Index", string.Format("The value of RouteValues[\"Action\"] should be 'Index' but it is '{0}'", viewResult.RouteValues["Action"]));
        }

        [Test]
        public void ShouldRenderEditViewByPOSTForAnUpdatedAndInvalidMovie()
        {
            // Arrange
            const int id = 1;

            HttpContextBase testContext = new TestHttpContext("~/Movies/Edit/1", "POST");
            ControllerContext context = new ControllerContext(new RequestContext(testContext, new RouteData()), _controller);
            _controller.ControllerContext = context;

            var fakeForm = new FormCollection { { "ID", "1" }, { "Title", "When Harry Met Sally1" }, { "Genre", "Chic movie" }, { "Year", "2004" }, { "Image", "~/Content/logo.png" } };
            _controller.ValueProvider = fakeForm.ToValueProvider();
            _controller.ViewData.ModelState.AddModelError("Key", "ErrorMessage");

            // Act
            ActionResult actual = _controller.EditPost(id);

            // Assert
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<ViewResult>(actual, "The returned ActionResult was not an instance of ViewResult");

            ViewResult viewResult = (ViewResult) actual;
            Assert.IsNotNull(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is Null");
            Assert.IsInstanceOf<Movie>(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is not an instance of Comment");

            Movie model = (Movie) viewResult.ViewData.Model;
            Assert.IsTrue(model.ID == id, string.Format("The value of Model.Id should be '{0}' but it is '{1}'", id, model.ID));
            Assert.IsTrue(model.Title == "When Harry Met Sally1", string.Format("The value of Model.Title should be '{0}' but it is '{1}'", "When Harry Met Sally1", model.Title));
            Assert.IsTrue(model.Genre == "Chic movie", string.Format("The value of Model.Genre should be '{0}' but it is '{1}'", "Chic movie", model.Genre));
            Assert.IsTrue(model.Year == 2004, string.Format("The value of Model.Year should be '{0}' but it is '{1}'", 2004, model.Year));
        }
            
        #endregion

        #region Tests when Delete actions execute

        [Test]
        public void ShouldRenderDeleteViewByGETForAExistingMovie()
        {
            // Arrange
            const int id = 3;
            Movie movie = _inMemoryMoviesService.Get(id);

            // Act
            ActionResult actual = _controller.Delete(id);

            // Assert
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<ViewResult>(actual, "The returned ActionResult was not an instance of ViewResult");

            ViewResult viewResult = (ViewResult) actual;
            Assert.IsNotNull(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is Null");
            Assert.IsInstanceOf<Movie>(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is not an instance of Movie");

            Movie model = (Movie) viewResult.ViewData.Model;
            Assert.IsTrue(model.ID == movie.ID, string.Format("The value of Model.ID should be '{0}' but it is '{1}'", movie.ID, model.ID));
            Assert.IsTrue(model.Director == movie.Director, string.Format("The value of Model.Director should be '{0}' but it is '{1}'", movie.Director, model.Director));
            Assert.IsTrue(model.Actors == movie.Actors, string.Format("The value of Model.Actors should be '{0}' but it is '{1}'", movie.Actors, model.Actors));
            Assert.IsTrue(model.Genre == movie.Genre, string.Format("The value of Model.Genre should be '{0}' but it is '{1}'", movie.Genre, model.Genre));
            Assert.IsTrue(model.Image == movie.Image, string.Format("The value of Model.Image should be '{0}' but it is '{1}'", movie.Image, model.Image));
            Assert.IsTrue(model.Runtime == movie.Runtime, string.Format("The value of Model.Runtime should be '{0}' but it is '{1}'", movie.Runtime, model.Runtime));
            Assert.IsTrue(model.Title == movie.Title, string.Format("The value of Model.Title should be '{0}' but it is '{1}'", movie.Title, model.Title));
            Assert.IsTrue(model.Year == movie.Year, string.Format("The value of Model.Year should be '{0}' but it is '{1}'", movie.Year, model.Year));
        }

        [Test]
        public void ShouldRenderTheNotFoundViewForDeletingANonExistingMovie()
        {
            // Arrange
            const int id = 10;

            // Act
            ActionResult actual = _controller.Delete(id);

            // Assert
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<ViewResult>(actual, "The returned ActionResult was not an instance of ViewResult");

            ViewResult viewResult = (ViewResult) actual;
            Assert.IsNotNull(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is Null");
            Assert.IsInstanceOf<NotFoundViewModel>(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is not an instance of NotFoundViewModel");

            NotFoundViewModel model = (NotFoundViewModel) viewResult.ViewData.Model;
            Assert.IsTrue(model.Id == id, string.Format("The value of Model.Id should be '{0}' but it is '{1}'", id, model.Id));
            Assert.IsTrue(model.Entity == "Movie", string.Format("The value of Model.Entity should be 'Movie' but it is '{0}'", model.Entity));
        }

        [Test]
        public void ShouldRenderDeleteViewByPOSTForAnExistingMovie()
        {
            // Arrange
            const int id = 3;
            int moviesCount = _inMemoryMoviesService.GetAll().Count;

            // Act
            ActionResult actual = _controller.DeletePost(id);

            // Assert
            Assert.IsTrue(_inMemoryMoviesService.GetAll().Count == moviesCount - 1, string.Format("The Movies count should be '{0}' but it is '{1}", moviesCount - 1, _inMemoryMoviesService.GetAll().Count));
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<RedirectToRouteResult>(actual, "The returned ActionResult was not an instance of RedirectToRouteResult");

            RedirectToRouteResult viewResult = (RedirectToRouteResult) actual;
            Assert.IsNotNull(viewResult.RouteName == "Default", string.Format("The RedirectToRouteResult RouteName should be 'Default' but it is '{0}'", viewResult.RouteName));
            Assert.IsTrue(viewResult.RouteValues["Action"].ToString() == "Index", string.Format("The value of RouteValues[\"Action\"] should be 'Index' but it is '{0}'", viewResult.RouteValues["Action"]));
        }

        #endregion
    }
}