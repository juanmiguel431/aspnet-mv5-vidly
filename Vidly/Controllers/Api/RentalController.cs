using System.Web.Http;
using Vidly.Dtos;

namespace Vidly.Controllers.Api
{
    public class RentalController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Add(RentalDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var location = $"{Request.RequestUri.AbsoluteUri}/{model.Id}";
            return Created(location, model);
        }
    }
}