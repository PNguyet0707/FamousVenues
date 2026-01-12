using AutoMapper;
using DataLayer.Dtos.Request;
using DataLayer.Dtos.Response;
using DataLayer.Entities;
using DataLayer.UnitOfWorks.Interfaces;
using ServiceLayer.Interfaces;

namespace ServiceLayer.Implementations
{
    public class ChefService : IChefService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ChefService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ChefCreationResponse> CreateChef(ChefCreationRequest request)
        {
            var chef = _mapper.Map<Chef>(request);
            chef.Id = Guid.NewGuid();
            chef.Revenue = 0m;
            chef.StartDate = DateTime.UtcNow.Ticks;

            var rows = await _unitOfWork.ChefRepository.AddAsync(chef);
            if (rows <= 0)
                return null;

            var response = _mapper.Map<ChefCreationResponse>(chef);
            _unitOfWork.Commit();
            return response;
        }
        public async Task<Chef> GetChefByIdAsync(Guid id)
        {
            var chef = await _unitOfWork.ChefRepository.GetByIdAsync(id);
            if (chef is null)
                return null;
            return chef;
        }
    }
}