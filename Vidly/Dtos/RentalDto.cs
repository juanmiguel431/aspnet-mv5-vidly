using System.Collections.Generic;

namespace Vidly.Dtos
{
    public class RentalDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public List<int> MovieIds { get; set; }
    }
}