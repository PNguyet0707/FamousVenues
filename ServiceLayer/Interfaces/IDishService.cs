using DataLayer.Dtos.Request;
using DataLayer.Dtos.Response;

namespace ServiceLayer.Interfaces
{
    public interface IDishService
    {
        public Task<DishCreationResponse> CreateDish(DishCreationRequest request);
        public Task<List<DishCreationResponse>> GetAllDishes();
    }
}
