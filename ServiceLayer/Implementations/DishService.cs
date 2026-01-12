using AutoMapper;
using DataLayer.Dtos.Request;
using DataLayer.Dtos.Response;
using DataLayer.Entities;
using DataLayer.UnitOfWorks.Interfaces;
using ServiceLayer.Exceptions;
using ServiceLayer.Interfaces;

namespace ServiceLayer.Implementations
{
    public class DishService : IDishService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DishService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DishCreationResponse> CreateDish(DishCreationRequest request)
        {
            var dish = _mapper.Map<Dish>(request);
            dish.Id = Guid.NewGuid();
            dish.CreatedAt = DateTime.UtcNow.Ticks;

            await _unitOfWork.DishRepository.AddAsync(dish);
            var chef = await _unitOfWork.ChefRepository.GetByIdAsync(dish.ChefId);
            if (chef is null)
                return null;

            var newRevenue = chef.Revenue + dish.Price;
            var affectedChefRows = await _unitOfWork.ChefRepository.UpdateRevenueAsync(chef.Id, newRevenue);
            if (affectedChefRows <= 0)
                return null;
            _unitOfWork.Commit();
            return new DishCreationResponse()
            {
                Name = dish.Name,
                ChefName = chef.Name,
                Review = dish.Review,
                Price = dish.Price,
            };
        }

        public async Task<List<DishCreationResponse>> GetAllDishes()
        {
            try
            {
                var dishes = await _unitOfWork.DishRepository.GetAllDishesAsync();
                return _mapper.Map<List<DishCreationResponse>>(dishes);
            }
            catch(Exception ex)
            {
                throw new DatabaseException();
            }
           
        }
    }
}
