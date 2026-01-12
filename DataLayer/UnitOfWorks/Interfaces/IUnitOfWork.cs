

using DataLayer.Repositories.Interfaces;

namespace DataLayer.UnitOfWorks.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IDishRepository DishRepository { get; }
        IChefRepository ChefRepository { get; }
        void Commit();
        
    }
}
