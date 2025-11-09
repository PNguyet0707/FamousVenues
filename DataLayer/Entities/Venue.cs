using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class Venue
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public VenueType VenueType { get; set; } 
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int CapacityStanding { get; set; }
        public int CapacitySeated { get; set; }
        public double Rating { get; set; }
        public int ReviewCount { get; set; }
        public int PriceRange { get; set; }
        public string ImageName { get; set; } 
    }
    public enum VenueType
    {
        None = 0,
        Hotel = 1,
        Restaurant = 2,
        Zoo = 3,
        Bar = 4,
        Ballroom = 5,
        FunctionVenue = 6,
        Aquarium = 7,
    }

}
