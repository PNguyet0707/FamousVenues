using Dapper;
using DataLayer.Dtos.Response;
using DataLayer.Entities;
using DataLayer.Extentions;
using DataLayer.Repositories.Interfaces;
using System.Data;
namespace DataLayer.Repositories.Implementations
{
    public class DishRepository : BaseRepository, IDishRepository
    {
        public DishRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public async Task<int> AddAsync(Dish entity)
        {
            var affectedRows = await Connection.ExecuteAsync(SqlQueries.CreateDish, entity, Transaction);
            return affectedRows;
        }

        public Task<int> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<DishCreationResponse>> GetAllDishesAsync()
        {
            try
            {
                var dishes = await Connection.QueryAsync<DishCreationResponse>(SqlQueries.GetAllDishes,  Transaction);
                return dishes.ToList();
            } 
            catch (Exception ex) 
            {
                throw;
            }
        }

        public Task<Dish> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Dish entity)
        {
            throw new NotImplementedException();
        }

        Task<IReadOnlyList<Dish>> IRepository<Dish>.GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
