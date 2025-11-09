using DataLayer.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Dtos.Request
{
    public class VenueUpdateRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public VenueType  VenueType { get; set; }
        public string  Address { get; set; }
        [Range(-90, 90)]
        public double Latitude { get; set; }
        [Range(-180, 180)]
        public double Longitude { get; set; }
        public int CapacityStanding { get; set; }
        public int CapacitySeated { get; set; }
        public int PriceRange { get; set; }
        public IFormFile? Image { get; set; }
    }
}
