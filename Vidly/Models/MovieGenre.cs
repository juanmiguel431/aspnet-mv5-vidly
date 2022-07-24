using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class MovieGenre
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}