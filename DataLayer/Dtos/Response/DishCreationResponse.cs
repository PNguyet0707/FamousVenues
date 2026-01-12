using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Dtos.Response
{
    public class DishCreationResponse
    {
        public string Name { get; set; }
        public string ChefName { get; set; }
        public int Review {  get; set; }
        public decimal Price { get; set; }
    }
}
