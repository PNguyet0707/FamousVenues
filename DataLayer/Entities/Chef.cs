using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Chef
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Andress { get; set; }
        public string PhoneNumber { get; set; }   
        public Gender Gender { get; set; }
        public decimal Revenue { get; set; }
        public long StartDate { get; set; }
    }
    public enum Gender
    {
        Unknown = 0,
        Male = 1,
        Female = 2,
        Other = 3
    }
}
