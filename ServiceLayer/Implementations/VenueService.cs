using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataLayer.Data;
using DataLayer.Dtos;
using DataLayer.Dtos.Request;
using DataLayer.Dtos.Response;
using DataLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ServiceLayer.Services
{
    public class VenueService(AppDbContext context, IMapper mapper) : IVenueService
    {
        public async Task<VenueCreationResponse> CreateVenue(VenueCreationRequest request, string imagePath)
        {
            var venue = new Venue()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Address = request.Address,
                VenueType = request.VenueType,
                PriceRange = request.PriceRange,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                CapacitySeated = request.CapacitySeated,
                CapacityStanding = request.CapacityStanding,
            };
            venue.ImageName = await SaveVenueImage(request.Image, imagePath);
            context.Venues.Add(venue);
            await context.SaveChangesAsync();
            return new VenueCreationResponse
            {
                Id = venue.Id,
                Name = venue.Name,
                Address = venue.Address,
            };
        }
       
        public async Task<List<VenueResponse>>  GetVenues(VenuesRequest request, string baseUrl)
        {
            var result =  await context.Venues.Where(v => v.Longitude >= request.LongMin && v.Longitude <= request.LongMax
                                                          && v.Latitude >= request.LatMin && v.Latitude <= request.LatMax)
                                        .Skip(request.Skip * request.Top)
                                        .Take(request.Top)
                                        .ProjectTo<VenueResponse>(mapper.ConfigurationProvider)
                                        .ToListAsync();
            result.ForEach(r => r.ImageUrl = $"{baseUrl}/VenueImages/{r.ImageUrl}");
            return result;
        }

        public async Task<bool> UpdateVenue(VenueUpdateRequest request, string imagePath)
        {
            var venue = await context.Venues.FindAsync(request.Id);
            if(venue is null)
            {
                return false;
            }
            if(request.Image is not null)
            {
                DeleteVenueImage(imagePath, venue.ImageName);
                venue.ImageName = await SaveVenueImage(request.Image, imagePath);
            }
            venue.Name = request.Name;
            venue.VenueType = request.VenueType;
            venue.Address = request.Address;
            venue.Longitude = request.Longitude;
            venue.Latitude = request.Latitude;
            venue.CapacityStanding = request.CapacityStanding;
            venue.CapacitySeated = request.CapacitySeated;
            venue.PriceRange = request.PriceRange; 
            await context.SaveChangesAsync();
            return true;

        }
        public async Task<bool> DeleteVenueById(Guid venueId, string imagePath)
        {
            var venue = await context.Venues.FindAsync(venueId);
            if(venue is null)
            {
                return false;
            }
            context.Venues.Remove(venue);
            DeleteVenueImage(imagePath, venue.ImageName);
            var deletedRow = await context.SaveChangesAsync();
            return deletedRow > 0;
        }
        public async Task<VenueRatingResponse> RateVenueAsync(RateVenueRequest request)
        {
            var venue = await context.Venues.FindAsync(request.VenueId);
            if (venue is null)
                return null;
            var currentAverage = venue.Rating;
            var currentCount = venue.ReviewCount;
            var newRatings = (currentAverage * currentCount + request.Rating) / (currentCount + 1);
            venue.Rating = Double.Round(newRatings, 2);
            venue.ReviewCount = currentCount + 1;
            await context.SaveChangesAsync();
            return new VenueRatingResponse()
            { 
                VenueId = venue.Id,
                AverageRating = venue.Rating,
                ReviewCount = venue.ReviewCount
            };
        }
        private async Task<string> SaveVenueImage(IFormFile imageFile, string imagePath)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return string.Empty;
            }
            var uploadsFolder = Path.Combine(imagePath, "VenueImages");
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }
            return fileName;
        }
        private void DeleteVenueImage(string imagePath, string imageName)
        {          
            var filePath = Path.Combine(imagePath, "VenueImages",imageName);
            if(System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
        
    }
}
