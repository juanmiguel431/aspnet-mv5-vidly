using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Rental
    {
        public int Id { get; set; }
        
        public Customer Customer { get; set; }
        
        [Required]
        public long CustomerId { get; set; }
        
        public Movie Movie { get; set; }
        
        [Required]
        public int MovieId { get; set; }
        
        [Required, Display(Name = "Date rented")]
        public DateTime DateRented { get; set; }
        
        [Display(Name = "Date returned")]
        public DateTime? DateReturned { get; set; }
    }
}