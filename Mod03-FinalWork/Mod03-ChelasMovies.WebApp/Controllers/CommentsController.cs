using System;
using System.Web.Mvc;
using Mod03_ChelasMovies.DomainModel.Entities;
using Mod03_ChelasMovies.DomainModel.Services;
using Mod03_ChelasMovies.WebApp.Models.ViewModels;

namespace Mod03_ChelasMovies.WebApp.Controllers
{
    public class CommentsController : Controller
    {
        #region Fields

        private readonly ICommentsService _commentsService;

        #endregion Fields

        #region Constructors

        public CommentsController(ICommentsService commentsService)
        {
            this._commentsService = commentsService;
        }

        #endregion Constructors

        #region Actions

        // GET: /Comments/Index/5
        [ChildActionOnly]
        public ActionResult Index(int movieId)
        {
            var movieComments = this._commentsService.GetAllCommentsFromMovie(movieId);
            return View(movieComments);
        }

        // GET: /Comments/Details/5
        public ActionResult Details(int id)
        {
            Comment comment = this._commentsService.Get(id);
            return comment == null ? View("NotFound", new NotFoundViewModel(id, "Comment")) : View(comment);
        }

        // GET: /Comments/Create
        public ActionResult Create(int movieId)
        {
            Comment c = new Comment { MovieID = movieId };
            return View(c);
        }

        // POST: /Comments/Create
        [HttpPost]
        public ActionResult Create(Comment c)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this._commentsService.Add(c);
                    return RedirectToRoute("Default", new { controller = "Movies", action = "Details", id = c.MovieID });
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", String.Format("Edit Failure, inner exception: {0}", e));
            }

            return View(c);
        }

        // GET: /Comments/Edit/5
        public ActionResult Edit(int id)
        {
            Comment comment = this._commentsService.Get(id);
            return View(comment);
        }

        // POST: /Comments/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult EditPost(int id)
        {
            Comment c = this._commentsService.Get(id);

            try
            {
                this.TryUpdateModel(c);

                if (ModelState.IsValid)
                {
                    this._commentsService.Update(c);
                    return RedirectToRoute("Default", new { controller = "Movies", action = "Details", id = c.MovieID });
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", String.Format("Edit Failure, inner exception: {0}", e));
            }

            return View(c);
        }

        // GET: /Comments/Delete/5
        public ActionResult Delete(int id)
        {
            Comment comment = this._commentsService.Get(id);
            return comment == null ? View("NotFound", new NotFoundViewModel(id, "Comment")) : View(comment);
        }

        // POST: /Comments/Delete/5
        [HttpPost]
        public ActionResult Delete(Comment c)
        {
            var movieId = c.MovieID;
            this._commentsService.Delete(c.ID);
            return RedirectToRoute("Default", new { controller = "Movies", action = "Details", id = movieId });
        }

        #endregion Actions
    }
}