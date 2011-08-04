using System;
using System.Web.Mvc;
using Mod03_ChelasMovies.DomainModel;
using Mod03_ChelasMovies.DomainModel.Services;
using Mod03_ChelasMovies.WebApp.Config;
using Mod03_ChelasMovies.WebApp.Models.ViewModels;

namespace Mod03_ChelasMovies.WebApp.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService moviesService;
        private readonly ICommentsService commentsService;

        public MoviesController(IMoviesService moviesService, ICommentsService commentsService)
        {
            this.moviesService = moviesService;
            this.commentsService = commentsService;
        }

        #region Movies

        //
        // GET: /Movies/
        public ActionResult Index(SearchViewModel searchModel)
        {
            searchModel.SearchResults = moviesService.GetAll(searchModel.Page, AppConfiguration.PageSize, searchModel.SortingCriteria);
            return View(searchModel);
        }

        //
        // GET: /Movies/Details/5
        public ActionResult Details(int id)
        {
            Movie movie = moviesService.Get(id);

            if (movie == null)
            {
                return View("NotFound", id);
            }

            return View(movie);
        }

        //
        // GET: /Movies/Create
        public ActionResult Create()
        {
            return View(new Movie());
        }

        //
        // POST: /Movies/Create
        [HttpPost]
        public ActionResult Create(string fillButton, string title)
        {
            if (!String.IsNullOrEmpty(fillButton) && !String.IsNullOrEmpty(title))
            {
                Movie tempMovie = moviesService.Search(title);
                ModelState.Clear();
                return View(tempMovie);
            }

            Movie newMovie = new Movie();
            TryUpdateModel(newMovie);

            if (ModelState.IsValid)
            {
                moviesService.Add(newMovie);
                return RedirectToAction("Details", new { id = newMovie.ID });
            }
            else
            {
                return View(newMovie);
            }
        }

        //
        // GET: /Movies/Edit/5
        public ActionResult Edit(int id)
        {
            Movie movie = moviesService.Get(id);
            return View(movie);
        }

        //
        // POST: /Movies/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult EditPost(int id)
        {
            Movie movie = moviesService.Get(id);

            try
            {
                this.UpdateModel(movie);

                if (ModelState.IsValid)
                {
                    moviesService.Update(movie);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", String.Format("Edit Failure, inner exception: {0}", e));

            }

            return View(movie);
        }

        //
        // GET: /Movies/Delete/5
        public ActionResult Delete(int id)
        {
            Movie movie = moviesService.Get(id);
            return movie == null ? View("NotFound", id) : View(movie);
        }

        //
        // POST: /Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            moviesService.Delete(id);
            return RedirectToAction("Index");
        }

        #endregion

        #region Comments

        // GET
        public ActionResult EditComment(int commentId)
        {
            Comment comment = this.commentsService.Get(commentId);
            return View(comment);
        }

        // POST
        [HttpPost]
        public ActionResult EditComment(Comment comment)
        {
            Comment c = this.commentsService.Get(comment.ID);

            try
            {
                this.UpdateModel(c);

                if (ModelState.IsValid)
                {
                    this.commentsService.Update(c);
                    return RedirectToRoute("Default", new { action = "Details", id = comment.MovieID });
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", String.Format("Edit Failure, inner exception: {0}", e));
            }

            return View(c);
        }

        public ActionResult DeleteComment(int movieId, int commentId)
        {
            Comment comment = this.commentsService.Get(commentId);
            return comment == null ? View("NotFound", commentId) : View(comment);
        }

        [HttpPost]
        public ActionResult DeleteComment(Comment c, int commentId)
        {
            commentsService.Delete(commentId);
            return RedirectToRoute("Default", new { action = "Details", id = c.MovieID });
        }

        public ActionResult CreateComment(int movieId)
        {
            Comment c = new Comment { MovieID = movieId };
            return View(c);
        }

        [HttpPost]
        public ActionResult CreateComment(Comment c)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Movie movie = moviesService.Get(c.MovieID);
                    movie.Comments.Add(c);
                    moviesService.Update(movie);
                    return RedirectToRoute("Default", new { action = "Details", id = c.MovieID });
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", String.Format("Edit Failure, inner exception: {0}", e));
            }

            return View(c);
        }

        public ActionResult DetailsComment(int commentId)
        {
            Comment comment = this.commentsService.Get(commentId);

            if (comment == null)
            {
                return View("NotFound", commentId);
            }

            return View(comment);
        }

        #endregion
    }
}