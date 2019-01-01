using H_Plus_Sports.Contracts;
using H_Plus_Sports.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace H_Plus_Sports.Repositories
{
    public class SalespersonRepository : ISalespersonRepository
    {
        private H_Plus_SportsContext _context;

        public SalespersonRepository(H_Plus_SportsContext context)
        {
            _context = context;
        }

        public IEnumerable<Salesperson> GetAll()
        {
            return _context.Salesperson;
        }

        public async Task<Salesperson> Add(Salesperson salesperson)
        {
            await _context.Salesperson.AddAsync(salesperson);
            await _context.SaveChangesAsync();
            return salesperson;
        }

        public async Task<Salesperson> Find(int id)
        {
            return await _context.Salesperson.Include(salesperson => salesperson.Order).SingleOrDefaultAsync(a => a.SalespersonId == id);
        }

        public async Task<Salesperson> Remove(int id)
        {
            var salesperson = await _context.Salesperson.SingleAsync(a => a.SalespersonId == id);
            _context.Salesperson.Remove(salesperson);
            await _context.SaveChangesAsync();
            return salesperson;
        }

        public async Task<Salesperson> Update(Salesperson salesperson)
        {
            _context.Salesperson.Update(salesperson);
            await _context.SaveChangesAsync();
            return salesperson;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Order.AnyAsync(e => e.OrderId == id);
        }
    }
}