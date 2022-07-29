using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public MoviesController(IMapper mapper)
        {
            _mapper = mapper;
            _context = new ApplicationDbContext();
        }
        
        // GET api/Movies
        public IEnumerable<MovieDto> GetAll()
        {
            return _context.Movies.ToList().Select(m => _mapper.Map<MovieDto>(m));
        }
        
        //GET api/Movies/1
        public IHttpActionResult GetById(int id)
        {
            var movie = _context.Movies.SingleOrDefault(p => p.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(movie);
        }
        
        //POST api/Movies
        [HttpPost]
        public IHttpActionResult Add(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = _mapper.Map<Movie>(movieDto);

            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri($"{Request.RequestUri}/{movie.Id}"), movieDto);
        }
        
        // PUT api/Movies/1
        [HttpPut]
        public MovieDto Update(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _mapper.Map(movieDto, movie);

            _context.SaveChanges();
            
            movieDto.Id = id;

            return movieDto;
        }
        
        // DELETE api/Movies/1
        [HttpDelete]
        public void Delete(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Movies.Remove(movie);
            _context.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}