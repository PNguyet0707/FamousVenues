using DataLayer.Dtos.Request;
using DataLayer.Dtos.Response;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IChefService
    {
        Task<ChefCreationResponse> CreateChef(ChefCreationRequest request);
    }
}