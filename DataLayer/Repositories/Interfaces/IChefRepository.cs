using DataLayer.Entities;

namespace DataLayer.Repositories.Interfaces
{
    public interface IChefRepository : IRepository<Chef>
    {
        Task<int> UpdateRevenueAsync(Guid id, decimal newRevenue);
    }
}
