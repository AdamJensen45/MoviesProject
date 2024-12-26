using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services.Bases;
using BLL.Models;
using BLL.DAL;
using Microsoft.AspNetCore.Authorization;
using BLL.Services;

// Generated from Custom Template.

namespace MoviesProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DirectorsController : MvcController
    {
        // Service injections:
        private readonly IService<Director, DirectorModel> _directorService;
        private readonly IService<Movie, MovieModel> _movieService;
        /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
        //private readonly IService<{Entity}, {Entity}Model> _{Entity}Service;

        public DirectorsController(
            IService<Director, DirectorModel> directorService
            , IService<Movie, MovieModel> movieService
        /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
        //, Service<{Entity}, {Entity}Model> {Entity}Service
        )
        {
            _directorService = directorService;
            _movieService = movieService;

            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            //_{Entity}Service = {Entity}Service;
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):

            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            //ViewBag.{Entity}Ids = new MultiSelectList(_{Entity}Service.Query().ToList(), "Record.Id", "Name");
        }

        // GET: Directors
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _directorService.Query().ToList();
            return View(list);
        }

        // GET: Directors/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _directorService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // GET: Directors/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Directors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DirectorModel director)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _directorService.Create(director.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = director.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(director);
        }

        // GET: Directors/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _directorService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: Directors/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DirectorModel director)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _directorService.Update(director.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = director.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(director);
        }

        // GET: Directors/Delete/5
        public IActionResult Delete(int id)
        {
            var item = _directorService.Query().SingleOrDefault(q => q.Record.Id == id);

            // Initialize ViewBag.HasDependencies with a default value
            ViewBag.HasDependencies = false;

            // Only update if there are movies
            var hasMovies = _movieService.Query().Any(m => m.Record.DirectorId == id);
            if (hasMovies)
            {
                ViewBag.HasDependencies = true;
                ViewBag.DependencyMessage = "This director has associated movies. Deleting will remove all movie relationships.";
            }
            return View(item);
        }

        // POST: Directors/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _directorService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}