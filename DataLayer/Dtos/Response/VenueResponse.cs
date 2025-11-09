using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Dtos
{
    public class VenueResponse
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
        public string ImageUrl { get; set; }
    }
}
