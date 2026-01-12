using DataLayer.Dtos.Response;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Interfaces
{
    public interface IDishRepository : IRepository<Dish>
    {
        public Task<IReadOnlyList<DishCreationResponse>> GetAllDishesAsync();

    }
   
}
