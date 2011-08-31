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
    public class GroupsControllerTests
    {
        #region Fields

        private IGroupsService _inMemoryGroupsService;
        private IMoviesService _inMemoryMoviesService;
        private GroupsController _controller;

        #endregion Fields

        #region Setup

        [SetUp]
        public void SetUp()
        {
            _inMemoryGroupsService = new InMemoryGroupsService();
            _inMemoryMoviesService = new InMemoryMoviesService();
            _controller = new GroupsController(_inMemoryGroupsService, _inMemoryMoviesService);
        }

        #endregion

        #region Tests when Index action executes

        [Test]
        public void ShouldRenderAllCreatedGroupsAndEnrolledGroupsForAnExistingUser()
        {
            // Arrange
            IPrincipal fakeUser = new GenericPrincipal(new GenericIdentity("User1", "Forms"), null);

            // Act
            ActionResult actual = _controller.Index(fakeUser);

            // Assert
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<ViewResult>(actual, "The returned ActionResult was not an instance of ViewResult");

            ViewResult viewResult = (ViewResult) actual;
            Assert.IsNotNull(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is Null");
            Assert.IsInstanceOf<GroupsIndexViewModel>(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is not an instance of GroupsIndexViewModel");

            GroupsIndexViewModel model = (GroupsIndexViewModel) viewResult.ViewData.Model;
            Assert.IsTrue(model.EnrolledGroups.Count == 1, string.Format("The value of model.EnrolledGroups.Count should be '{0}' but it is '{1}'", 1, model.EnrolledGroups.Count));
            Assert.IsTrue(model.OwnedGroups.Count == 1, string.Format("The value of model.OwnedGroups.Count should be '{0}' but it is '{1}'", 1, model.OwnedGroups.Count));
        }

        [Test]
        public void ShouldRenderEmptyCreatedGroupsAndEnrolledGroupsForAnNonExistingUser()
        {
            // Arrange
            IPrincipal fakeUser = new GenericPrincipal(new GenericIdentity("User4", "Forms"), null);

            // Act
            ActionResult actual = _controller.Index(fakeUser);

            // Assert
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<ViewResult>(actual, "The returned ActionResult was not an instance of ViewResult");

            ViewResult viewResult = (ViewResult) actual;
            Assert.IsNotNull(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is Null");
            Assert.IsInstanceOf<GroupsIndexViewModel>(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is not an instance of GroupsIndexViewModel");

            GroupsIndexViewModel model = (GroupsIndexViewModel) viewResult.ViewData.Model;
            Assert.IsTrue(model.EnrolledGroups.Count == 0, string.Format("The value of model.EnrolledGroups.Count should be '{0}' but it is '{1}'", 0, model.EnrolledGroups.Count));
            Assert.IsTrue(model.OwnedGroups.Count == 0, string.Format("The value of model.OwnedGroups.Count should be '{0}' but it is '{1}'", 0, model.OwnedGroups.Count));
        }

        [Test]
        public void ShouldRenderEmptyCreatedGroupsAndEnrolledGroupsForAnExistingUserWithoutGroups()
        {
            // Arrange
            IPrincipal fakeUser = new GenericPrincipal(new GenericIdentity("User3", "Forms"), null);

            // Act
            ActionResult actual = _controller.Index(fakeUser);

            // Assert
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<ViewResult>(actual, "The returned ActionResult was not an instance of ViewResult");

            ViewResult viewResult = (ViewResult) actual;
            Assert.IsNotNull(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is Null");
            Assert.IsInstanceOf<GroupsIndexViewModel>(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is not an instance of GroupsIndexViewModel");

            GroupsIndexViewModel model = (GroupsIndexViewModel) viewResult.ViewData.Model;
            Assert.IsTrue(model.EnrolledGroups.Count == 0, string.Format("The value of model.EnrolledGroups.Count should be '{0}' but it is '{1}'", 0, model.EnrolledGroups.Count));
            Assert.IsTrue(model.OwnedGroups.Count == 0, string.Format("The value of model.OwnedGroups.Count should be '{0}' but it is '{1}'", 0, model.OwnedGroups.Count));
        }

        #endregion Tests when Index action executes

        #region Tests when Details action executes

        [Test]
        public void ShouldRenderTheDetailsForAnExistingGroup()
        {
            // Arrange
            const int id = 1;
            Group group = _inMemoryGroupsService.Get(id);
            UsersDetailsViewModel usersDetailsViewModel = new UsersDetailsViewModel();
            MoviesDetailsViewModel moviesDetailsViewModel = new MoviesDetailsViewModel();
            IPrincipal fakeUser = new GenericPrincipal(new GenericIdentity("User1", "Forms"), null);


            // Act
            ActionResult actual = _controller.Details(id, usersDetailsViewModel, moviesDetailsViewModel, fakeUser);

            // Assert
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<ViewResult>(actual, "The returned ActionResult was not an instance of ViewResult");

            ViewResult viewResult = (ViewResult) actual;
            Assert.IsNotNull(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is Null");
            Assert.IsInstanceOf<Group>(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is not an instance of Group");

            Group model = (Group) viewResult.ViewData.Model;
            Assert.IsTrue(model.ID == group.ID, string.Format("The value of Model.ID should be '{0}' but it is '{1}'", group.ID, model.ID));
            Assert.IsTrue(model.Name == group.Name, string.Format("The value of Model.Name should be '{0}' but it is '{1}'", group.Name, model.Name));
            Assert.IsTrue(model.Description == group.Description, string.Format("The value of Model.Description should be '{0}' but it is '{1}'", group.Description, model.Description));
        }

        #endregion Tests when Details action executes

        #region Tests when Create actions execute

        [Test]
        public void ShouldRenderCreateViewByGETForANewGroup()
        {
            // Act
            ActionResult actual = _controller.Create();

            // Assert
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<ViewResult>(actual, "The returned ActionResult was not an instance of ViewResult");

            ViewResult viewResult = (ViewResult) actual;
            Assert.IsNotNull(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is Null");
            Assert.IsInstanceOf<Group>(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is not an instance of Group");

            Group model = (Group) viewResult.ViewData.Model;
            Assert.IsTrue(model.ID == 0, string.Format("The value of Model.ID should be '{0}' but it is '{1}'", 0, model.ID));
        }

        [Test]
        public void ShouldRenderMovieDetailsViewByPOSTForANewValidGroup()
        {
            // Arrange
            IPrincipal fakeUser = new GenericPrincipal(new GenericIdentity("User1", "Forms"), null);
            Group newGroup = new Group {Name = "Test Group", Description = "Just a test group"};
            const int newGroupId = 4;

            // Act
            ActionResult actual = _controller.Create(newGroup, fakeUser);

            // Assert
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<RedirectToRouteResult>(actual, "The returned ActionResult was not an instance of RedirectToRouteResult");
            Assert.IsTrue(newGroup.ID == newGroupId, string.Format("The value of newGroup.ID should be '{0}' but it is '{1}'", newGroupId, newGroup.ID));

            RedirectToRouteResult viewResult = (RedirectToRouteResult) actual;
            Assert.IsNotNull(viewResult.RouteName == "Default", string.Format("The RedirectToRouteResult RouteName should be 'Default' but it is '{0}'", viewResult.RouteName));
            Assert.IsTrue(viewResult.RouteValues["Action"].ToString() == "Index", string.Format("The value of RouteValues[\"Action\"] should be 'Index' but it is '{0}'", viewResult.RouteValues["Action"]));
        }

        [Test]
        public void ShouldRenderCreateViewByPOSTForAnInvalidGroup()
        {
            // Arrange
            IPrincipal fakeUser = new GenericPrincipal(new GenericIdentity("User1", "Forms"), null);
            Group newGroup = new Group { Name = "Test Group", Description = "Just a test group" };
            _controller.ViewData.ModelState.AddModelError("Key", "ErrorMessage");

            // Act
            ActionResult actual = _controller.Create(newGroup, fakeUser);

            // Assert
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<ViewResult>(actual, "The returned ActionResult was not an instance of ViewResult");

            ViewResult viewResult = (ViewResult) actual;
            Assert.IsNotNull(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is Null");
            Assert.IsInstanceOf<Group>(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is not an instance of Group");

            Group model = (Group) viewResult.ViewData.Model;
            Assert.IsTrue(model.ID == newGroup.ID, string.Format("The value of Model.ID should be '{0}' but it is '{1}'", newGroup.ID, model.ID));
            Assert.IsTrue(model.Name == newGroup.Name, string.Format("The value of Model.Name should be '{0}' but it is '{1}'", newGroup.Name, model.Name));
            Assert.IsTrue(model.Description == newGroup.Description, string.Format("The value of Model.Description should be '{0}' but it is '{1}'", newGroup.Description, model.Description));
        }

        #endregion Tests when Create actions execute

        #region Tests when Edit actions execute

        [Test]
        public void ShouldRenderEditViewByGETForAExistingGroup()
        {
            // Arrange
            const int id = 1;
            Group group = this._inMemoryGroupsService.Get(id);

            // Act
            ActionResult actual = _controller.Edit(id);

            // Assert
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<ViewResult>(actual, "The returned ActionResult was not an instance of ViewResult");

            ViewResult viewResult = (ViewResult) actual;
            Assert.IsNotNull(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is Null");
            Assert.IsInstanceOf<Group>(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is not an instance of Group");

            Group model = (Group) viewResult.ViewData.Model;
            Assert.IsTrue(model.ID == group.ID, string.Format("The value of Model.ID should be '{0}' but it is '{1}'", group.ID, model.ID));
            Assert.IsTrue(model.Name == group.Name, string.Format("The value of Model.Name should be '{0}' but it is '{1}'", group.Name, model.Name));
            Assert.IsTrue(model.Description == group.Description, string.Format("The value of Model.Description should be '{0}' but it is '{1}'", group.Description, model.Description));
        }

        [Test]
        public void ShouldRenderEditViewByPOSTForANewUpdatedAndValidGroup()
        {
            // Arrange
            const int groupId = 2;

            HttpContextBase testContext = new TestHttpContext("~/Groups/Edit/5", "POST");
            ControllerContext context = new ControllerContext(new RequestContext(testContext, new RouteData()), _controller);
            _controller.ControllerContext = context;

            var fakeForm = new FormCollection { { "ID", "2" }, { "Description", "Description 2 Updated" }, { "Name", "Test Group 2" } };
            _controller.ValueProvider = fakeForm.ToValueProvider();

            // Act
            ActionResult actual = _controller.EditPost(groupId);

            // Assert
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<RedirectToRouteResult>(actual, "The returned ActionResult was not an instance of RedirectToRouteResult");

            RedirectToRouteResult viewResult = (RedirectToRouteResult) actual;
            Assert.IsNotNull(viewResult.RouteName == "Default", string.Format("The RedirectToRouteResult RouteName should be 'Default' but it is '{0}'", viewResult.RouteName));
            Assert.IsTrue(viewResult.RouteValues["Action"].ToString() == "Index", string.Format("The value of RouteValues[\"Action\"] should be 'Index' but it is '{0}'", viewResult.RouteValues["Action"]));
        }

        [Test]
        public void ShouldRenderEditViewByPOSTForAnUpdatedAndInvalidGroup()
        {
            // Arrange
            const int groupId = 2;

            HttpContextBase testContext = new TestHttpContext("~/Groups/Edit/5", "POST");
            ControllerContext context = new ControllerContext(new RequestContext(testContext, new RouteData()), _controller);
            _controller.ControllerContext = context;

            var fakeForm = new FormCollection { { "ID", "2" }, { "Description", "Description 2 Updated" }, { "Name", "Test Group 2" } };
            _controller.ValueProvider = fakeForm.ToValueProvider();
            _controller.ViewData.ModelState.AddModelError("Key", "ErrorMessage");

            // Act
            ActionResult actual = _controller.EditPost(groupId);

            // Assert
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<ViewResult>(actual, "The returned ActionResult was not an instance of ViewResult");

            ViewResult viewResult = (ViewResult) actual;
            Assert.IsNotNull(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is Null");
            Assert.IsInstanceOf<Group>(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is not an instance of Group");

            Group model = (Group) viewResult.ViewData.Model;
            Assert.IsTrue(model.ID == groupId, string.Format("The value of Model.Id should be '{0}' but it is '{1}'", groupId, model.ID));
            Assert.IsTrue(model.Description == "Description 2 Updated", string.Format("The value of Model.Description should be '{0}' but it is '{1}'", "Description 2 Updated", model.Description));
            Assert.IsTrue(model.Name == "Test Group 2", string.Format("The value of Model.Name should be '{0}' but it is '{1}'", "Test Group 2", model.Name));
        }

        #endregion Tests when Edit actions execute

        #region Tests when Delete actions execute

        [Test]
        public void ShouldRenderDeleteViewByGETForAExistingGroup()
        {
            // Arrange
            const int id = 1;
            Group group = this._inMemoryGroupsService.Get(id);

            // Act
            ActionResult actual = _controller.Delete(id);

            // Assert
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<ViewResult>(actual, "The returned ActionResult was not an instance of ViewResult");

            ViewResult viewResult = (ViewResult) actual;
            Assert.IsNotNull(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is Null");
            Assert.IsInstanceOf<Group>(viewResult.ViewData.Model, "The value of viewResult.ViewData.Model is not an instance of Group");

            Group model = (Group) viewResult.ViewData.Model;
            Assert.IsTrue(model.ID == group.ID, string.Format("The value of Model.ID should be '{0}' but it is '{1}'", group.ID, model.ID));
            Assert.IsTrue(model.Name == group.Name, string.Format("The value of Model.Rating should be '{0}' but it is '{1}'", group.Name, model.Name));
            Assert.IsTrue(model.Description == group.Description, string.Format("The value of Model.Description should be '{0}' but it is '{1}'", group.Description, model.Description));
        }

        [Test]
        public void ShouldRenderTheNotFoundViewForDeletingANonExistingGroup()
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
            Assert.IsTrue(model.Entity == "Group", string.Format("The value of Model.Entity should be 'Group' but it is '{0}'", model.Entity));
        }

        [Test]
        public void ShouldRenderDeleteViewByPOSTForAnExistingGroup()
        {
            // Arrange
            const int groupId = 3;
            int groupsCount = this._inMemoryGroupsService.GetAll().Count;

            // Act
            ActionResult actual = _controller.DeletePost(groupId);

            // Assert
            Assert.IsTrue(this._inMemoryGroupsService.GetAll().Count == groupsCount - 1, string.Format("The Groups count should be '{0}' but it is '{1}", groupsCount - 1, this._inMemoryGroupsService.GetAll().Count));
            Assert.IsNotNull(actual, "The returned ActionResult is Null");
            Assert.IsInstanceOf<RedirectToRouteResult>(actual, "The returned ActionResult was not an instance of RedirectToRouteResult");

            RedirectToRouteResult viewResult = (RedirectToRouteResult) actual;
            Assert.IsNotNull(viewResult.RouteName == "Default", string.Format("The RedirectToRouteResult RouteName should be 'Default' but it is '{0}'", viewResult.RouteName));
            Assert.IsTrue(viewResult.RouteValues["Action"].ToString() == "Index", string.Format("The value of RouteValues[\"Action\"] should be 'Index' but it is '{0}'", viewResult.RouteValues["Action"]));
        }

        #endregion Tests when Delete actions execute
    }
}