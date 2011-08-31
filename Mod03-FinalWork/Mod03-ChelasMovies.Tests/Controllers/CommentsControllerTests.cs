using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Mod03_ChelasMovies.DomainModel;
using Mod03_ChelasMovies.DomainModel.Entities;
using Mod03_ChelasMovies.DomainModel.Services;
using Mod03_ChelasMovies.DomainModel.ServicesImpl;
using Mod03_ChelasMovies.Tests.Utils;
using Mod03_ChelasMovies.WebApp.Controllers;
using Mod03_ChelasMovies.WebApp.Models.ViewModels;
using NUnit.Framework;
using System.Web;

namespace Mod03_ChelasMovies.Tests.Controllers
{
    [TestFixture]
    public class CommentsControllerTests
    {
        #region Fields

        private IMoviesService _inMemoryMoviesService;
        private ICommentsService _inMemoryCommentsService;
        private CommentsController _controller;

        #endregion Fields

        #region Setup

        [SetUp]
        public void SetUp()
        {
            _inMemoryMoviesService = new InMemoryMoviesService();
            _inMemoryCommentsService = new InMemoryCommentsService();
            _controller = new CommentsController(_inMemoryCommentsService);
        }

        #endregion Setup

        #region Tests when Index action executes

        [Test]
        public void ShouldRenderAllCommentsForAnExistingMovie()
        {
            // Arrange
            const int movieId = 2;

            // Act
            ActionResult actual = _controller.Index(movieId);

            // Assert
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<ViewResult>(actual, "The returned ActionResult was not an instance of ViewResult");

            ViewResult viewResult = (ViewResult) actual;
            Assert.IsNotNull(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is Null");
            Assert.IsInstanceOf<ICollection<Comment>>(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is not an instance of ICollection<Comment>");

            ICollection<Comment> model = (ICollection<Comment>) viewResult.ViewData.Model;
            Assert.IsTrue(model.Count == 2, string.Format("The value of model.Count should be '{0}' but it is '{1}'", 2, model.Count));

            List<Comment> modelList = (List<Comment>) model;
            Assert.IsTrue(modelList[0].ID == 3, string.Format("The value of modelList[0].ID should be '{0}' but it is '{1}'", 3, modelList[0].ID));
            Assert.IsTrue(modelList[1].ID == 4, string.Format("The value of modelList[1].ID should be '{0}' but it is '{1}'", 4, modelList[1].ID));
        }

        [Test]
        public void ShouldRenderNoCommentsForAnExistingMovieThatHasNoComments()
        {
            // Arrange
            const int movieId = 3;

            // Act
            ActionResult actual = _controller.Index(movieId);

            // Assert
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<ViewResult>(actual, "The returned ActionResult was not an instance of ViewResult");

            ViewResult viewResult = (ViewResult) actual;
            Assert.IsNotNull(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is Null");
            Assert.IsInstanceOf<ICollection<Comment>>(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is not an instance of ICollection<Comment>");

            ICollection<Comment> model = (ICollection<Comment>) viewResult.ViewData.Model;
            Assert.IsTrue(model.Count == 0, string.Format("The value of model.Count should be '{0}' but it is '{1}'", 0, model.Count));
        }

        [Test]
        public void ShouldRenderNoCommentsForANonExistingMovie()
        {
            // Arrange
            const int movieId = 10;

            // Act
            ActionResult actual = _controller.Index(movieId);

            // Assert
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<ViewResult>(actual, "The returned ActionResult was not an instance of ViewResult");

            ViewResult viewResult = (ViewResult) actual;
            Assert.IsNotNull(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is Null");
            Assert.IsInstanceOf<ICollection<Comment>>(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is not an instance of ICollection<Comment>");

            ICollection<Comment> model = (ICollection<Comment>) viewResult.ViewData.Model;
            Assert.IsTrue(model.Count == 0, string.Format("The value of model.Count should be '{0}' but it is '{1}'", 0, model.Count));
        }

        #endregion Tests when Index action executes

        #region Tests when Details action executes

        [Test]
        public void ShouldRenderTheDetailsForAnExistingComment()
        {
            // Arrange
            const int id = 3;
            Comment comment = _inMemoryCommentsService.Get(id);

            // Act
            ActionResult actual = _controller.Details(id);

            // Assert
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<ViewResult>(actual, "The returned ActionResult was not an instance of ViewResult");

            ViewResult viewResult = (ViewResult) actual;
            Assert.IsNotNull(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is Null");
            Assert.IsInstanceOf<Comment>(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is not an instance of Comment");

            Comment model = (Comment) viewResult.ViewData.Model;
            Assert.IsTrue(model.ID == comment.ID, string.Format("The value of Model.ID should be '{0}' but it is '{1}'", comment.ID, model.ID));
            Assert.IsTrue(model.Rating == comment.Rating, string.Format("The value of Model.Rating should be '{0}' but it is '{1}'", comment.Rating, model.Rating));
            Assert.IsTrue(model.Description == comment.Description, string.Format("The value of Model.Description should be '{0}' but it is '{1}'", comment.Description, model.Description));
        }

        [Test]
        public void ShouldRenderTheNotFoundViewForANonExistingComment()
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
            Assert.IsTrue(model.Entity == "Comment", string.Format("The value of Model.Entity should be 'Comment' but it is '{0}'", model.Entity));
        }

        #endregion Tests when Details action executes

        #region Tests when Create actions execute

        [Test]
        public void ShouldRenderCreateViewByGETForANewComment()
        {
            // Arrange
            const int movieId = 3;

            // Act
            ActionResult actual = _controller.Create(movieId);

            // Assert
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<ViewResult>(actual, "The returned ActionResult was not an instance of ViewResult");

            ViewResult viewResult = (ViewResult) actual;
            Assert.IsNotNull(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is Null");
            Assert.IsInstanceOf<Comment>(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is not an instance of Comment");

            Comment model = (Comment) viewResult.ViewData.Model;
            Assert.IsTrue(model.MovieID == movieId, string.Format("The value of Model.MovieID should be '{0}' but it is '{1}'", movieId, model.MovieID));
            Assert.IsTrue(model.ID == 0, string.Format("The value of Model.ID should be '{0}' but it is '{1}'", 0, model.ID));
        }

        [Test]
        public void ShouldRenderMovieDetailsViewByPOSTForANewValidComment()
        {
            // Arrange
            const int movieId = 2;
            Movie movie = _inMemoryMoviesService.Get(movieId);
            Comment newComment = new Comment {MovieID = movieId, Rating = (int) Rating.Good, Description = "Description 5", Movie = movie};

            // Act
            ActionResult actual = _controller.Create(newComment);

            // Assert
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<RedirectToRouteResult>(actual, "The returned ActionResult was not an instance of RedirectToRouteResult");
            Assert.IsTrue(newComment.ID == 5, string.Format("The value of newComment.ID should be '{0}' but it is '{1}'", 5, newComment.ID));

            RedirectToRouteResult viewResult = (RedirectToRouteResult )actual;
            Assert.IsNotNull(viewResult.RouteName == "Default", string.Format("The RedirectToRouteResult RouteName should be 'Default' but it is '{0}'", viewResult.RouteName));
            Assert.IsTrue(viewResult.RouteValues["Controller"].ToString() == "Movies", string.Format("The value of RouteValues[\"Controller\"] should be 'Movies' but it is '{0}'", viewResult.RouteValues["Controller"]));
            Assert.IsTrue(viewResult.RouteValues["Action"].ToString() == "Details", string.Format("The value of RouteValues[\"Action\"] should be 'Details' but it is '{0}'", viewResult.RouteValues["Action"]));
            Assert.IsTrue((int)viewResult.RouteValues["id"] == movieId, string.Format("The RouteValues[\"id\"] should be '{0}' but it is '{1}'", movieId, viewResult.RouteValues["id"]));
        }

        [Test]
        public void ShouldRenderCreateViewByPOSTForAnInvalidComment()
        {
            // Arrange
            const int movieId = 2;
            Comment newComment = new Comment { MovieID = movieId, Rating = (int)Rating.Good, Description = "Description 5" };
            _controller.ViewData.ModelState.AddModelError("Key", "ErrorMessage");

            // Act
            ActionResult actual = _controller.Create(newComment);

            // Assert
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<ViewResult>(actual, "The returned ActionResult was not an instance of ViewResult");

            ViewResult viewResult = (ViewResult) actual;
            Assert.IsNotNull(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is Null");
            Assert.IsInstanceOf<Comment>(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is not an instance of Comment");

            Comment model = (Comment) viewResult.ViewData.Model;
            Assert.IsTrue(model.MovieID == newComment.MovieID, string.Format("The value of Model.MovieID should be '{0}' but it is '{1}'", newComment.MovieID, model.MovieID));
            Assert.IsTrue(model.ID == newComment.ID, string.Format("The value of Model.ID should be '{0}' but it is '{1}'", newComment.ID, model.ID));
            Assert.IsTrue(model.Rating == newComment.Rating, string.Format("The value of Model.Rating should be '{0}' but it is '{1}'", newComment.Rating, model.Rating));
            Assert.IsTrue(model.Description == newComment.Description, string.Format("The value of Model.Description should be '{0}' but it is '{1}'", newComment.Description, model.Description));
        }

        #endregion Tests when Create actions execute

        #region Tests when Edit actions execute

        [Test]
        public void ShouldRenderEditViewByGETForAExistingComment()
        {
            // Arrange
            const int id = 1;
            Comment comment = _inMemoryCommentsService.Get(id);

            // Act
            ActionResult actual = _controller.Edit(id);

            // Assert
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<ViewResult>(actual, "The returned ActionResult was not an instance of ViewResult");

            ViewResult viewResult = (ViewResult) actual;
            Assert.IsNotNull(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is Null");
            Assert.IsInstanceOf<Comment>(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is not an instance of Comment");

            Comment model = (Comment)viewResult.ViewData.Model;
            Assert.IsTrue(model.ID == comment.ID, string.Format("The value of Model.ID should be '{0}' but it is '{1}'", comment.ID, model.ID));
            Assert.IsTrue(model.Rating == comment.Rating, string.Format("The value of Model.Rating should be '{0}' but it is '{1}'", comment.Rating, model.Rating));
            Assert.IsTrue(model.Description == comment.Description, string.Format("The value of Model.Description should be '{0}' but it is '{1}'", comment.Description, model.Description));
        }

        [Test]
        public void ShouldRenderEditViewByPOSTForANewUpdatedAndValidComment()
        {
            // Arrange
            const int commentId = 2;

            HttpContextBase testContext = new TestHttpContext("~/Comments/Edit/5", "POST");
            ControllerContext context = new ControllerContext(new RequestContext(testContext, new RouteData()), _controller);
            _controller.ControllerContext = context;

            var fakeForm = new FormCollection { { "ID", "2" }, { "Description", "Description 2 Updated" }, { "Rating", "3" }, { "MovieId", "1" } };
            _controller.ValueProvider = fakeForm.ToValueProvider();

            // Act
            ActionResult actual = _controller.EditPost(commentId);

            // Assert
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<RedirectToRouteResult>(actual, "The returned ActionResult was not an instance of RedirectToRouteResult");

            RedirectToRouteResult viewResult = (RedirectToRouteResult) actual;
            Assert.IsNotNull(viewResult.RouteName == "Default", string.Format("The RedirectToRouteResult RouteName should be 'Default' but it is '{0}'", viewResult.RouteName));
            Assert.IsTrue(viewResult.RouteValues["Controller"].ToString() == "Movies", string.Format("The value of RouteValues[\"Controller\"] should be 'Movies' but it is '{0}'", viewResult.RouteValues["Controller"]));
            Assert.IsTrue(viewResult.RouteValues["Action"].ToString() == "Details", string.Format("The value of RouteValues[\"Action\"] should be 'Details' but it is '{0}'", viewResult.RouteValues["Action"]));
            Assert.IsTrue((int)viewResult.RouteValues["id"] == 1, string.Format("The RouteValues[\"id\"] should be '{0}' but it is '{1}'", 1, viewResult.RouteValues["id"]));
        }

        [Test]
        public void ShouldRenderEditViewByPOSTForAnUpdatedAndInvalidComment()
        {
            // Arrange
            const int commentId = 2;

            HttpContextBase testContext = new TestHttpContext("~/Comments/Edit/5", "POST");
            ControllerContext context = new ControllerContext(new RequestContext(testContext, new RouteData()), _controller);
            _controller.ControllerContext = context;

            var fakeForm = new FormCollection { { "ID", "2" }, { "Description", "Description 2 Updated" }, { "Rating", "3" }, { "MovieID", "1" } };
            _controller.ValueProvider = fakeForm.ToValueProvider();
            _controller.ViewData.ModelState.AddModelError("Key", "ErrorMessage");

            // Act
            ActionResult actual = _controller.EditPost(commentId);

            // Assert
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<ViewResult>(actual, "The returned ActionResult was not an instance of ViewResult");

            ViewResult viewResult = (ViewResult) actual;
            Assert.IsNotNull(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is Null");
            Assert.IsInstanceOf<Comment>(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is not an instance of Comment");

            Comment model = (Comment) viewResult.ViewData.Model;
            Assert.IsTrue(model.ID == commentId, string.Format("The value of Model.Id should be '{0}' but it is '{1}'", commentId, model.ID));
            Assert.IsTrue(model.Description == "Description 2 Updated", string.Format("The value of Model.Description should be '{0}' but it is '{1}'", "Description 2 Updated", model.Description));
            Assert.IsTrue(model.Rating == 3, string.Format("The value of Model.Rating should be '{0}' but it is '{1}'", 3, model.Rating));
            Assert.IsTrue(model.MovieID == 1, string.Format("The value of Model.MovieID should be '{0}' but it is '{1}'", 1, model.MovieID));
        }

        #endregion Tests when Edit actions execute

        #region Tests when Delete actions execute

        [Test]
        public void ShouldRenderDeleteViewByGETForAExistingComment()
        {
            // Arrange
            const int id = 1;
            Comment comment = _inMemoryCommentsService.Get(id);

            // Act
            ActionResult actual = _controller.Delete(id);

            // Assert
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<ViewResult>(actual, "The returned ActionResult was not an instance of ViewResult");

            ViewResult viewResult = (ViewResult) actual;
            Assert.IsNotNull(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is Null");
            Assert.IsInstanceOf<Comment>(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is not an instance of Comment");

            Comment model = (Comment)viewResult.ViewData.Model;
            Assert.IsTrue(model.ID == comment.ID, string.Format("The value of Model.ID should be '{0}' but it is '{1}'", comment.ID, model.ID));
            Assert.IsTrue(model.Rating == comment.Rating, string.Format("The value of Model.Rating should be '{0}' but it is '{1}'", comment.Rating, model.Rating));
            Assert.IsTrue(model.Description == comment.Description, string.Format("The value of Model.Description should be '{0}' but it is '{1}'", comment.Description, model.Description));
        }

        [Test]
        public void ShouldRenderTheNotFoundViewForDeletingANonExistingComment()
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
            Assert.IsTrue(model.Entity == "Comment", string.Format("The value of Model.Entity should be 'Comment' but it is '{0}'", model.Entity));
        }

        [Test]
        public void ShouldRenderDeleteViewByPOSTForAnExistingComment()
        {
            // Arrange
            const int commentId = 4;
            int commentsCount = _inMemoryCommentsService.GetAll().Count;
            Comment commentToDelete = _inMemoryCommentsService.Get(commentId);
            
            // Act
            ActionResult actual = _controller.Delete(commentToDelete);

            // Assert
            Assert.IsTrue(_inMemoryCommentsService.GetAll().Count == commentsCount - 1, string.Format("The Comments count should be '{0}' but it is '{1}", commentsCount - 1, _inMemoryCommentsService.GetAll().Count));
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<RedirectToRouteResult>(actual, "The returned ActionResult was not an instance of RedirectToRouteResult");

            RedirectToRouteResult viewResult = (RedirectToRouteResult) actual;
            Assert.IsNotNull(viewResult.RouteName == "Default", string.Format("The RedirectToRouteResult RouteName should be 'Default' but it is '{0}'", viewResult.RouteName));
            Assert.IsTrue(viewResult.RouteValues["Controller"].ToString() == "Movies", string.Format("The value of RouteValues[\"Controller\"] should be 'Movies' but it is '{0}'", viewResult.RouteValues["Controller"]));
            Assert.IsTrue(viewResult.RouteValues["Action"].ToString() == "Details", string.Format("The value of RouteValues[\"Action\"] should be 'Details' but it is '{0}'", viewResult.RouteValues["Action"]));
            Assert.IsTrue((int)viewResult.RouteValues["id"] == commentToDelete.MovieID, string.Format("The RouteValues[\"id\"] should be '{0}' but it is '{1}'", 1, viewResult.RouteValues["id"]));
        }

        #endregion Tests when Delete actions execute
    }
}