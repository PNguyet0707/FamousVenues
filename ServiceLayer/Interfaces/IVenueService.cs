using DataLayer.Dtos;
using DataLayer.Dtos.Request;
using DataLayer.Dtos.Response;
namespace ServiceLayer.Services
{
    public interface IVenueService
    {
        Task<VenueCreationResponse> CreateVenue(VenueCreationRequest request, string imagePath);
        Task<bool> DeleteVenueById(Guid venueId, string imagePath);
        Task<bool> UpdateVenue(VenueUpdateRequest request, string imagePath);
        Task<List<VenueResponse>> GetVenues(VenuesRequest request, string baseUrl);
        Task<VenueRatingResponse> RateVenueAsync(RateVenueRequest request);
    }
}
