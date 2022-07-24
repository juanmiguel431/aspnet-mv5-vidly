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
        public byte MovieGenreId { get; set; }
        public DateTime ReleasedDate { get; set; }
        public DateTime AddedDate { get; set; }
        
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }
    }
}