using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;

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

        private IQueryable<Customer> GetCustomers()
        {
            return _context.Customers;
            // return new List<Customer>()
            // {
            //     new Customer() { Id = 1, Name = "John Smith" },
            //     new Customer() { Id = 2, Name = "Mary Williams" }
            // };
        }

        // GET
        public ViewResult Index()
        {
            var customers = GetCustomers().ToList();
            return View(customers);
        }

        public ActionResult Details(int id)
        {
            var customer = GetCustomers().SingleOrDefault(p => p.Id == id);
            if (customer == null) return HttpNotFound();

            return View(customer);
        }
    }
}