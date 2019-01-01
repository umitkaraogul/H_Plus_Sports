using H_Plus_Sports.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace H_Plus_Sports.Contracts
{
    public interface ISalespersonRepository
    {
        Task<Salesperson> Add(Salesperson item);

        IEnumerable<Salesperson> GetAll();

        Task<Salesperson> Find(int id);

        Task<Salesperson> Remove(int id);

        Task<Salesperson> Update(Salesperson item);

        Task<bool> Exists(int id);
    }
}