using H_Plus_Sports.Contracts;
using H_Plus_Sports.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace H_Plus_Sports.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private H_Plus_SportsContext _context;

        public OrderItemRepository(H_Plus_SportsContext context)
        {
            _context = context;
        }

        public IEnumerable<OrderItem> GetAll()
        {
            return _context.OrderItem;
        }

        public async Task<OrderItem> Add(OrderItem orderItem)
        {
            await _context.OrderItem.AddAsync(orderItem);
            await _context.SaveChangesAsync();
            return orderItem;
        }

        public async Task<OrderItem> Find(int id)
        {
            return await _context.OrderItem.Include(orderItem => orderItem.Order).Include(orderItem => orderItem.Product).SingleOrDefaultAsync(a => a.OrderItemId == id);
        }

        public async Task<OrderItem> Remove(int id)
        {
            var orderItem = await _context.OrderItem.SingleAsync(a => a.OrderItemId == id);
            _context.OrderItem.Remove(orderItem);
            await _context.SaveChangesAsync();
            return orderItem;
        }

        public async Task<OrderItem> Update(OrderItem orderItem)
        {
            _context.OrderItem.Update(orderItem);
            await _context.SaveChangesAsync();
            return orderItem;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.OrderItem.AnyAsync(e => e.OrderItemId == id);
        }
    }
}