using System.ComponentModel.DataAnnotations;

namespace DataLayer.Dtos.Request
{
    public class VenuesRequest
    {
        [Range(-180,180)]
        public double LongMax { get; set; }
        [Range(-180, 180)]
        public double LongMin { get; set; }
        [Range(-90, 90)]
        public double LatMax { get; set; }
        [Range(-90, 90)]
        public double LatMin { get; set; }
        public int Skip { get; set; }
        public int Top { get; set; }
    }
}
