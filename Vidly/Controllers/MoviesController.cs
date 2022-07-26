using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
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

        public ActionResult New()
        {
            var movieGenres = _context.MovieGenres.ToList();

            var model = new MovieFormViewModel
            {
                MovieGenres = movieGenres
            };
            return View("MovieForm", model);
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

        public ActionResult Edit(int movieId)
        {
            return Content($"id = {movieId}");
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
            var movies = _context.Movies.Include(p => p.MovieGenre).ToList();
            return View(movies);
        }
        
        public ActionResult Details(int id)
        {
            var movies = _context.Movies.Include(p => p.MovieGenre).SingleOrDefault(p => p.Id == id);
            if (movies == null) return HttpNotFound();

            return View(movies);
        }

        public ViewResult MovieForm()
        {
            var movieGenres = _context.MovieGenres.ToList();

            var model = new MovieFormViewModel
            {
                MovieGenres = movieGenres
            };
            return View(model);
        }

        public ActionResult Save(Movie movie)
        {
            if (movie.Id == 0)
            {
                movie.AddedDate = DateTime.UtcNow;
                _context.Movies.Add(movie);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}