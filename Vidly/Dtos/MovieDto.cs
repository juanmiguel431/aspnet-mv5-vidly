using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public byte MovieGenreId { get; set; }

        public MovieGenreDto MovieGenre { get; set; }
        
        [Required]
        public DateTime ReleasedDate { get; set; }
        
        public DateTime AddedDate { get; set; }
        
        [Required, Range(0, 500)]
        public int Stock { get; set; }
    }
}