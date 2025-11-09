namespace DataLayer.Dtos.Response
{
    public class VenueRatingResponse
    {
        public Guid VenueId { get; set; }
        public double AverageRating { get; set; } 
        public int ReviewCount { get; set; }
    }
}
