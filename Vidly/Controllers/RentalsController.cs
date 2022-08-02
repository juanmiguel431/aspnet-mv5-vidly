using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class RentalsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public RentalsController(IMapper mapper)
        {
            _mapper = mapper;
            _context = new ApplicationDbContext();
        }
        
        public ViewResult New()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}