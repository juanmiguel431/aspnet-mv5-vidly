using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {

        private readonly ApplicationDbContext _context;

        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>()
            {
                new Customer() { Id = 1, Name = "John Smith" },
                new Customer() { Id = 2, Name = "Mary Williams" }
            };
        }

        public ActionResult New()
        {
            var membershipsTypes = _context.MembershipTypes.ToList();
            var model = new NewCustomerViewModel
            {
                MembershipTypes = membershipsTypes
            };
            return View(model);
        }
        
        public ActionResult Create()
        {
            return View("New");
        }

        // GET
        public ViewResult Index()
        {
            var customers = _context.Customers.Include(p => p.MembershipType).ToList();
            return View(customers);
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(p => p.MembershipType).SingleOrDefault(p => p.Id == id);
            if (customer == null) return HttpNotFound();

            return View(customer);
        }
    }
}