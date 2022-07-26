using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public MovieGenre MovieGenre { get; set; }
        
        [Required, Display(Name = "Genre")]
        public byte MovieGenreId { get; set; }
        
        [Required, Display(Name = "Release date")]
        public DateTime ReleasedDate { get; set; }
        
        public DateTime AddedDate { get; set; }
        
        [Required, Range(0, int.MaxValue), Display(Name = "Number in stock")]
        public int Stock { get; set; }
    }
}