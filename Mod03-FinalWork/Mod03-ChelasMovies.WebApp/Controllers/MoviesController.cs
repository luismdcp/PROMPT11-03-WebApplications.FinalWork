using System;
using System.Security.Principal;
using System.Web.Mvc;
using Mod03_ChelasMovies.DomainModel.Entities;
using Mod03_ChelasMovies.DomainModel.Services;
using Mod03_ChelasMovies.WebApp.Config;
using Mod03_ChelasMovies.WebApp.Models.ViewModels;

namespace Mod03_ChelasMovies.WebApp.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        #region Fields

        private readonly IMoviesService _moviesService;
        private readonly IGroupsService _groupsService;

        #endregion Fields

        #region Constructors

        public MoviesController(IMoviesService moviesService, IGroupsService groupsService)
        {
            this._moviesService = moviesService;
            this._groupsService = groupsService;
        }

        #endregion Constructors

        #region Actions

        // GET: /Movies/
        public ActionResult Index(SearchViewModel searchModel, SortPagingViewModel sortPagingModel, IPrincipal user)
        {
            searchModel.SearchResults = this._moviesService.GetAllReachable(user.Identity.Name, searchModel.BuildFilter(), sortPagingModel.Page, AppConfiguration.PageSize, sortPagingModel.SortingCriteriaWithOrder);
            sortPagingModel.SearchResults = searchModel.SearchResults;
            SearchSortPagingViewModel indexModel = new SearchSortPagingViewModel(searchModel, sortPagingModel);

            return View(indexModel);
        }

        // GET: /Movies/Details/5
        public ActionResult Details(int id)
        {
            Movie movie = this._moviesService.Get(id);
            return movie == null ? View("NotFound", new NotFoundViewModel(id, "Movie")) : View(movie);
        }

        // GET: /Movies/Create
        public ActionResult Create()
        {
            return View(new Movie());
        }

        // POST: /Movies/Create
        [HttpPost]
        public ActionResult Create(string fillButton, string title, IPrincipal user)
        {
            if (!String.IsNullOrEmpty(fillButton) && !String.IsNullOrEmpty(title))
            {
                Movie tempMovie = this._moviesService.Search(title);
                ModelState.Clear();
                return View(tempMovie);
            }

            Movie newMovie = new Movie();
            TryUpdateModel(newMovie);

            if (ModelState.IsValid)
            {
                var owner = this._groupsService.GetUserInfo(user.Identity.Name);
                newMovie.Owner = owner;
                newMovie.OwnerId = owner.UserId;
                this._moviesService.Add(newMovie);
                return RedirectToAction("Details", new { id = newMovie.ID });
            }
            else
            {
                return View(newMovie);
            }
        }

        // GET: /Movies/Edit/5
        public ActionResult Edit(int id)
        {
            Movie movie = this._moviesService.Get(id);
            return View(movie);
        }

        // POST: /Movies/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult EditPost(int id)
        {
            Movie movie = this._moviesService.Get(id);

            try
            {
                this.TryUpdateModel(movie);

                if (ModelState.IsValid)
                {
                    this._moviesService.Update(movie);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", String.Format("Edit Failure, inner exception: {0}", e));

            }

            return View(movie);
        }

        // GET: /Movies/Delete/5
        public ActionResult Delete(int id)
        {
            Movie movie = this._moviesService.Get(id);
            return movie == null ? View("NotFound", new NotFoundViewModel(id, "Movie")) : View(movie);
        }

        // POST: /Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            this._moviesService.Delete(id);
            return RedirectToAction("Index");
        }

        #endregion Actions
    }
}