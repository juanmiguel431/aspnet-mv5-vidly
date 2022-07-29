using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public CustomersController(IMapper mapper)
        {
            _mapper = mapper;
            _context = new ApplicationDbContext();
        }

        // GET api/customers
        public IEnumerable<CustomerDto> GetAll()
        {
            return _context.Customers.ToList().Select(c => _mapper.Map<CustomerDto>(c));
        }
        
        // GET api/customers/1
        public CustomerDto GetById(int id)
        {
            var customer = _context.Customers.SingleOrDefault(p => p.Id == id);

            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return _mapper.Map<CustomerDto>(customer);
        }
        
        // POST api/customers
        [HttpPost]
        public CustomerDto Create(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customer = _mapper.Map<Customer>(customerDto);
            
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;
            
            return customerDto;
        }
        
        // PUT api/customers/1
        [HttpPut]
        public CustomerDto Update(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _mapper.Map(customerDto, customerInDb);

            _context.SaveChanges();

            return customerDto;
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
