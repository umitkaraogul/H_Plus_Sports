using H_Plus_Sports.Models;
using H_Plus_Sports.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace H_Plus_Sports.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private H_Plus_SportsContext _context;
        public CustomerRepository(H_Plus_SportsContext context)
        {
            _context = context;
        }
        public async Task<Customer> Add(Customer customer)
        {
            await _context.Customer.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<bool> Exist(int id)
        {
            return await _context.Customer.AnyAsync(customer => customer.CustomerId == id);
        }

        public async Task<Customer> Find(int id)
        {
            return await _context.Customer.Include(customer => customer.Order).SingleOrDefaultAsync(a => a.CustomerId == id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customer;
        }

        public async Task<Customer> Remove(int id)
        {
            var customer = await _context.Customer.SingleAsync(c => c.CustomerId == id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> Update(Customer customer)
        {
            _context.Customer.Update(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
    }
}