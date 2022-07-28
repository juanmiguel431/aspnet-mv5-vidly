using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private readonly ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        // GET api/customers
        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }
        
        // GET api/customers/1
        public Customer GetById(int id)
        {
            var customer = _context.Customers.SingleOrDefault(p => p.Id == id);

            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return customer;
        }
        
        // POST api/customers
        [HttpPost]
        public Customer Create(Customer customer)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return customer;
        }
        
        // PUT api/customers/1
        [HttpPut]
        public Customer Update(int id, Customer customer)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            
            customerInDb.Name = customer.Name;
            customerInDb.Birthdate = customer.Birthdate;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;
            customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            
            _context.SaveChanges();

            return customer;
        }
        
        // DELETE api/customers/1
        [HttpDelete]
        public void Delete(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Customers.Remove(customerInDb);
            
            _context.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }
    }
}
