using Dapper;
using DataLayer.Entities;
using DataLayer.Extentions;
using DataLayer.Repositories.Interfaces;
using System.Data;

namespace DataLayer.Repositories.Implementations
{
    public class ChefRepository : BaseRepository, IChefRepository
    {
        public ChefRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public async Task<int> AddAsync(Chef entity)
        {
            var affectedRows = await Connection.ExecuteAsync(SqlQueries.CreateChef, entity, Transaction);
            return affectedRows;
        }

        public Task<int> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Chef>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Chef> GetByIdAsync(Guid id)
        {
            var chef = await Connection.QuerySingleOrDefaultAsync<Chef>(SqlQueries.SelectChefById, new { Id = id }, Transaction);
            return chef;
        }

        public Task<int> UpdateAsync(Chef entity)
        {
            throw new NotImplementedException();
        }
        public async Task<int> UpdateRevenueAsync(Guid id, decimal newRevenue)
        {
            var affected = await Connection.ExecuteAsync(SqlQueries.UpdateChefRevenue, new { Id = id, Revenue = newRevenue }, Transaction);
            return affected;
        }
    }
}
