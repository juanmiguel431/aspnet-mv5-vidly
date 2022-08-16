using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Areas.Admin.Controllers
{
    // [RouteArea("Admin")]
    // [Route("Movies/{action}")]
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // private IEnumerable<Movie> GetMovies()
        // {
        //     return new List<Movie>()
        //     {
        //         new Movie() { Id = 1, Name = "Shrek" },
        //         new Movie() { Id = 2, Name = "Wall-e" }
        //     };
        // }

        // GET: Movies/Random
        public ViewResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };

            // var view = new ViewResult();

            // return View(movie);
            //return Content("Juan Miguel");
            //return HttpNotFound();
            //return new EmptyResult();
            // return RedirectToAction("Index", "Home", new { Page = 1, OrderBy = "Name" });
            // return Redirect("/Home");
            // return Json(movie, JsonRequestBehavior.AllowGet);

            var customers = new List<Customer>()
            {
                new Customer() { Name = "Customer 1" },
                new Customer() { Name = "Customer 2" },
                new Customer() { Name = "Customer 3" },
                new Customer() { Name = "Customer 4" },
                new Customer() { Name = "Customer 5" },
                new Customer() { Name = "Customer 6" },
            };

            var viewModel = new RandomMovieViewModel()
            {
                Movie = movie,
                Customers = customers
            };

            ViewData["Movie"] = movie;
            ViewBag.Movie = movie;

            var viewResult = new ViewResult
            {
                ViewData =
                {
                    Model = viewModel
                }
            };

            viewResult.ViewData["Movie"] = movie;
            // return viewResult;
            
            return View(viewModel);
        }

        // public ActionResult Index(int? pageIndex, string sortBy)
        // {
        //     if (!pageIndex.HasValue) pageIndex = 1;
        //
        //     if (string.IsNullOrWhiteSpace(sortBy)) sortBy = "Name";
        //
        //     return Content($"PageIndex={pageIndex}&SortBy={sortBy}");
        // }

        [Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1, 12)}")]
        public ActionResult ByReleaseDate(int year, byte month)
        {
            return Content($"Year:{year} Month:{month}");
        }        
        
        public ActionResult Index()
        {
            // var movies = _context.Movies.Include(p => p.MovieGenre).ToList();
            // return View(movies);

            if (User.IsInRole(RoleName.CanManageMovies))
                return View("List");

            return View("ReadOnlyList");
        }
        
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var movieGenres = _context.MovieGenres.ToList();

            var model = new MovieFormViewModel
            {
                MovieGenres = movieGenres,
            };
            return View("MovieForm", model);
        }
        
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.Single(p => p.Id == id);
            
            var movieGenres = _context.MovieGenres.ToList();
            
            var model = new MovieFormViewModel(movie)
            {
                MovieGenres = movieGenres,
            };
            return View("MovieForm", model);
        }
        
        public ActionResult Details(int id)
        {
            var movies = _context.Movies.Include(p => p.MovieGenre).SingleOrDefault(p => p.Id == id);
            if (movies == null) return HttpNotFound();

            return View(movies);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ViewResult MovieForm()
        {
            var movieGenres = _context.MovieGenres.ToList();

            var model = new MovieFormViewModel
            {
                MovieGenres = movieGenres
            };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(MovieFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.MovieGenres = _context.MovieGenres.ToList();
                return View("MovieForm", viewModel);
            }

            if (viewModel.Id == null)
            {
                var movie = new Movie
                {
                    Name = viewModel.Name,
                    Stock = viewModel.Stock ?? 0,
                    ReleasedDate = viewModel.ReleasedDate ?? DateTime.MinValue,
                    MovieGenreId = viewModel.MovieGenreId ?? 0,
                    AddedDate = DateTime.UtcNow
                };   
                
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(p => p.Id == viewModel.Id);
                movieInDb.Name = viewModel.Name;
                movieInDb.MovieGenreId = viewModel.MovieGenreId ?? 0;
                movieInDb.ReleasedDate = viewModel.ReleasedDate ?? DateTime.MinValue;
                movieInDb.Stock = viewModel.Stock ?? 0;
            }

            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException)
            {
                throw;
            }
            
            return RedirectToAction("Index");
        }
    }
}