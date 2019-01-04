using H_Plus_Sports.Contracts;
using H_Plus_Sports.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace H_Plus_Sports.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private H_Plus_SportsContext _context;
        private IMemoryCache _cache;

        public ProductRepository(H_Plus_SportsContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Product;
        }

        public async Task<Product> Add(Product product)
        {
            await _context.Product.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }
        public async Task<Product> Find(string id)
        {
            var cachedProduct = _cache.Get<Product>(id);

            if (cachedProduct != null)
            {
                return cachedProduct;
            }
            else
            {
                var dbProduct = await _context.Product.Include(product => product.OrderItem).SingleOrDefaultAsync(a => a.ProductId == id);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60));
                _cache.Set(dbProduct.ProductId, dbProduct, cacheEntryOptions);

                return dbProduct;
            }

        }

        public async Task<Product> Remove(string id)
        {
            var product = await _context.Product.SingleAsync(a => a.ProductId == id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Update(Product product)
        {
            _context.Product.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> Exists(string id)
        {
            return await _context.Product.AnyAsync(e => e.ProductId == id);
        }
    }
}