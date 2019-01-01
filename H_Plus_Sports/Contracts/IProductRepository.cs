using H_Plus_Sports.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace H_Plus_Sports.Contracts
{
    public interface IProductRepository
    {
        Task<Product> Add(Product item);

        IEnumerable<Product> GetAll();

        Task<Product> Find(string id);

        Task<Product> Remove(string id);

        Task<Product> Update(Product item);

        Task<bool> Exists(string id);
    }
}