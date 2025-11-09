using DataLayer.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Dtos
{
    public class VenueCreationRequest
    {
        public string Name { get; set; }
        public VenueType VenueType { get; set; }
        public string? Address { get; set; }
        [Range(-180, 180)]
        public double Longitude { get; set; }
        [Range(-90, 90)]
        public double Latitude { get; set; }
        public int CapacityStanding { get; set; }
        public int CapacitySeated { get; set; }
        public int PriceRange { get; set; }
        public IFormFile Image { get; set; }
    }
}
