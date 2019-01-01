using H_Plus_Sports.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace H_Plus_Sports.Contracts
{
    public interface IOrderItemRepository
    {
        Task<OrderItem> Add(OrderItem item);

        IEnumerable<OrderItem> GetAll();

        Task<OrderItem> Find(int id);

        Task<OrderItem> Remove(int id);

        Task<OrderItem> Update(OrderItem item);

        Task<bool> Exists(int id);
    }
}