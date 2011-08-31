using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using Mod03_ChelasMovies.DomainModel.Entities;
using Mod03_ChelasMovies.DomainModel.Services;
using Mod03_ChelasMovies.WebApp.Models.ViewModels;

namespace Mod03_ChelasMovies.WebApp.Controllers
{
    [Authorize]
    public class GroupsController : Controller
    {
        #region Fields

        private readonly IGroupsService _groupsService;
        private readonly IMoviesService _moviesService;

        #endregion Fields

        #region Constructors

        public GroupsController(IGroupsService groupsService, IMoviesService moviesService)
        {
            this._groupsService = groupsService;
            this._moviesService = moviesService;
        }

        #endregion Constructors

        #region Actions

        // GET: /Groups/Index
        public ActionResult Index(IPrincipal user)
        {
            var ownedGroups = this._groupsService.GetGroupsCreatedByUser(user.Identity.Name);
            var enrolledGroups = this._groupsService.GetEnrolledGroupsByUser(user.Identity.Name);
            GroupsIndexViewModel viewModel = new GroupsIndexViewModel(ownedGroups, enrolledGroups);
            return View(viewModel);
        }

        // GET: /Groups/Details/5
        public ActionResult Details(int id, UsersDetailsViewModel usersDetails, MoviesDetailsViewModel moviesDetails, IPrincipal user)
        {
            var group = this._groupsService.Get(id);

            if (group == null)
            {
                return View("NotFound", new NotFoundViewModel(id, "Group"));
            }
            else
            {
                if (!String.IsNullOrEmpty(usersDetails.AddUsersButton))
                {
                    var allUsers = this._groupsService.GetAllUsersExceptLoggedUser(user.Identity.Name);
                    var usersNotEnrolled = allUsers.Where(u => group.EnrolledUsers.Any(e => e.UserId == u.UserId) == false);

                    ViewBag.UsersNotEnrolled = usersNotEnrolled;
                    return View("_AddUsers", group);
                }

                if (!String.IsNullOrEmpty(usersDetails.DeleteUsersButton))
                {
                    ViewBag.DeleteEnrolledUsers = usersDetails.DeleteEnrolledUsers;
                    return View("_DeleteUsers", group);
                }

                if (!String.IsNullOrEmpty(moviesDetails.AddMoviesButton))
                {
                    var ownedMovies = this._moviesService.GetMoviesCreatedByUser(user.Identity.Name);
                    var moviesNotShared = ownedMovies.Where(m => group.SharedMovies.Any(s => s.ID == m.ID) == false);

                    ViewBag.MoviesNotShared = moviesNotShared;
                    return View("_AddMovies", group);
                }

                if (!String.IsNullOrEmpty(moviesDetails.DeleteMoviesButton))
                {
                    List<Movie> moviesBuffer = moviesDetails.DeleteSharedMovies.Select(deleteMovieId => this._moviesService.Get(deleteMovieId)).ToList();
                    ViewBag.DeleteSharedMovies = moviesBuffer;
                    return View("_DeleteMovies", group);
                }
            }

            return View(group);
        }

        // GET: /Groups/Create
        public ActionResult Create()
        {
            Group group = new Group();
            return View(group);
        } 

        // POST: /Groups/Create
        [HttpPost]
        public ActionResult Create(Group group, IPrincipal user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var owner = this._groupsService.GetUserInfo(user.Identity.Name);
                    group.Owner = owner;
                    group.OwnerId = owner.UserId;
                    this._groupsService.Add(group);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", String.Format("Edit Failure, inner exception: {0}", e));
            }

            return View(group);
        }
        
        // GET: /Groups/Edit/5
        public ActionResult Edit(int id)
        {
            Group group = this._groupsService.Get(id);
            return View(group);
        }

        // POST: /Groups/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult EditPost(int id)
        {
            Group group = this._groupsService.Get(id);

            try
            {
                this.TryUpdateModel(group);

                if (ModelState.IsValid)
                {
                    this._groupsService.Update(group);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", String.Format("Edit Failure, inner exception: {0}", e));
            }

            return View(group);
        }

        // GET: /Groups/Delete/5
        public ActionResult Delete(int id)
        {
            Group group = this._groupsService.Get(id);
            return group == null ? View("NotFound", new NotFoundViewModel(id, "Group")) : View(group);
        }

        // POST: /Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            this._groupsService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddUsers(int id, string[] addUsers)
        {
            var group = this._groupsService.Get(id);
            List<User> userBuffer = addUsers.Select(userName => this._groupsService.GetUserInfo(userName)).ToList();

            userBuffer.ForEach(u => group.EnrolledUsers.Add(u));

            this._groupsService.Update(group);
            return RedirectToAction("Details", new {id = id});
        }

        [HttpPost]
        public ActionResult DeleteUsers(int id, string[] deleteUsers)
        {
            var group = this._groupsService.Get(id);
            List<User> userBuffer = deleteUsers.Select(userName => this._groupsService.GetUserInfo(userName)).ToList();

            userBuffer.ForEach(u => group.EnrolledUsers.Remove(u));

            this._groupsService.Update(group);
            return RedirectToAction("Details", new { id = id });
        }

        [HttpPost]
        public ActionResult AddMovies(int id, int[] addMovies)
        {
            var group = this._groupsService.Get(id);
            List<Movie> moviesBuffer = addMovies.Select(addMovie => this._moviesService.Get(addMovie)).ToList();

            moviesBuffer.ForEach(m => group.SharedMovies.Add(m));

            this._groupsService.Update(group);
            return RedirectToAction("Details", new { id = id });
        }

        [HttpPost]
        public ActionResult DeleteMovies(int id, int[] deleteMovies)
        {
            var group = this._groupsService.Get(id);
            List<Movie> moviesBuffer = deleteMovies.Select(deleteMovie => this._moviesService.Get(deleteMovie)).ToList();

            moviesBuffer.ForEach(m => group.SharedMovies.Remove(m));

            this._groupsService.Update(group);
            return RedirectToAction("Details", new { id = id });
        }

        #endregion Actions
    }
}