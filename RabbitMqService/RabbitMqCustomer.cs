namespace RabbitMqService
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email {  get; set; }
        public int YearOfBirth { get; set; }
        public CustomerType CustomerType { get; set; }
        public string Country { get; set; }
    }

    public enum CustomerType
    {
        None = 0,
        All = 1,
        Enterprise = 2,
        Personal = 3,
    }
}
