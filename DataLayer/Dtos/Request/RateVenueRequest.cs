using System.ComponentModel.DataAnnotations;

namespace DataLayer.Dtos.Request
{
    public class RateVenueRequest
    {
        public Guid VenueId { get; set; }
        [Range(1,5)]
        public int Rating { get; set; } 
    }
}
