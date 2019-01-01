using H_Plus_Sports.Contracts;
using H_Plus_Sports.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace H_Plus_Sports.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private H_Plus_SportsContext _context;

        public OrderRepository(H_Plus_SportsContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Order;
        }

        public async Task<Order> Add(Order order)
        {
            await _context.Order.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> Find(int id)
        {
            return await _context.Order.Include(order => order.OrderItem).Include(order => order.Customer).SingleOrDefaultAsync(a => a.OrderId == id);
        }

        public async Task<Order> Remove(int id)
        {
            var order = _context.Order.Single(a => a.OrderId == id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> Update(Order order)
        {
            _context.Order.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Order.AnyAsync(e => e.OrderId == id);
        }
    }
}