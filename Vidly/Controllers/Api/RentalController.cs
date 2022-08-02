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

            var customer = _context.Customers.Single(p => p.Id == model.CustomerId);

            var movies = _context.Movies.Where(p => model.MovieIds.Contains(p.Id)).ToList();

            var rentals = new List<Rental>();
            foreach (var movieId in model.MovieIds)
            {
                var movie = movies.First(p => p.Id == movieId);
                
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