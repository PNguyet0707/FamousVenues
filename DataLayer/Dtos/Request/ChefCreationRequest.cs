using DataLayer.Entities;

namespace DataLayer.Dtos.Request
{
    public class ChefCreationRequest
    {
        public string Name { get; set; }
        public string Andress { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
    }
}   