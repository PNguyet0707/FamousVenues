namespace DataLayer.Extentions
{
    public static class SqlQueries
    {
        #region  DISHES
        public const string CreateDish =
            "INSERT INTO [Dishes] (Id, Name, ChefId, Category, Price, CreatedAt, Review) " +
            "VALUES (@Id, @Name, @ChefId, @Category, @Price, @CreatedAt, @Review);";

        public const string GetAllDishes =
            "SELECT Dishes.Name AS Name ,Review, Price, Chefs.Name AS ChefName  from Dishes, Chefs where dishes.ChefId = Chefs.Id ";
        #endregion

        #region CHEFS
        public const string CreateChef =
           "INSERT INTO [Chefs] (Id, Name, Andress, PhoneNumber, Gender, Revenue, StartDate) " +
           "VALUES (@Id, @Name, @Andress, @PhoneNumber, @Gender, @Revenue, @StartDate);";

        public const string SelectChefById =
            "SELECT Id, Name, Andress, PhoneNumber, Gender, Revenue, StartDate " +
            "FROM [Chefs] WHERE Id = @Id;";
        public const string UpdateChefRevenue =
           "UPDATE [Chefs] SET Revenue = @Revenue WHERE Id = @Id;";
        #endregion

    }
}
