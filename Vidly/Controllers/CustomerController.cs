using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {
        private List<Customer> Customers { get; } = new List<Customer>()
        {
            new Customer() { Id = 1, Name = "John Smith" },
            new Customer() { Id = 2, Name = "Mary Williams" }
        };

        // GET
        public ViewResult Index()
        {
            var model = new CustomerViewModel() { Customers = Customers };
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var customer = Customers.FirstOrDefault(p => p.Id == id);
            if (customer == null) return HttpNotFound();

            return View(customer);
        }
    }
}