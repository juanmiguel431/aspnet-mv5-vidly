using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class RentalController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public RentalController(IMapper mapper)
        {
            _mapper = mapper;
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Add(RentalDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (!model.MovieIds.Any())
                return BadRequest("You must specify unless one movie to rent.");
            
            var customer = _context.Customers.SingleOrDefault(p => p.Id == model.CustomerId);

            if (customer == null)
                return BadRequest("CustomerId is not valid.");

            var movies = _context.Movies.Where(p => model.MovieIds.Contains(p.Id)).ToList();

            if (model.MovieIds.Count != movies.Count)
                return BadRequest("One or more movies Id are invalid.");

            var rentals = new List<Rental>();
            foreach (var movie in movies)
            {
                if (movie.Available == 0)
                    return BadRequest($"Movie {movie.Id} is not available");

                movie.Available--;
                
                rentals.Add(new Rental
                {
                    Customer = customer,
                    Movie = movie
                });
            }

            _context.Rentals.AddRange(rentals);
            _context.SaveChanges();
            
            return Ok();
        }
    }
}