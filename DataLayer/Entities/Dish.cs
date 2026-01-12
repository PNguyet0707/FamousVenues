namespace DataLayer.Entities
{
    public class Dish
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ChefId { get; set; }
        public DishCategory Category { get; set; }
        public decimal Price { get; set; }
        public long CreatedAt { get; set; }
        public int Review {get; set; }
        
    }
    public enum DishCategory
    {
        Unknown = 0,
        Appetizer = 1,    
        MainCourse = 2,   
        Dessert = 3,    
        SideDish = 4,    
        Drink = 5        
    }
}
