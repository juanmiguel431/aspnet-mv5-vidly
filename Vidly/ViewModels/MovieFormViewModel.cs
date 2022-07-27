using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {
        public IEnumerable<MovieGenre> MovieGenres { get; set; }

        public int? Id { get; set; }
        
        [Required(ErrorMessage = "Please enter movie's name")]
        public string Name { get; set; }

        [Required, Display(Name = "Genre")]
        public byte? MovieGenreId { get; set; }
        
        [Required, Display(Name = "Release date")]
        public DateTime? ReleasedDate { get; set; }

        [Required, Range(0, 500), Display(Name = "Number in stock")]
        public int? Stock { get; set; }

        public string Title => Id.HasValue ? "Edit movie" : "New movie";

        public MovieFormViewModel()
        {
        }
        
        public MovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            Stock = movie.Stock;
            ReleasedDate = movie.ReleasedDate;
            MovieGenreId = movie.MovieGenreId;
        }
    }
}