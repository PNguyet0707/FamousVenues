using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Dtos.Request
{
    public class DishCreationRequest
    {
        public string Name { get; set; }
        public Guid ChefId { get; set; }
        public DishCategory Category { get; set; }
        public decimal Price { get; set; }
        public int Review { get; set; }

    }
}
