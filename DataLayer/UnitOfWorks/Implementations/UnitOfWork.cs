using DataLayer.Repositories.Implementations;
using DataLayer.Repositories.Interfaces;
using DataLayer.UnitOfWorks.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DataLayer.UnitOfWorks.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDishRepository _dishRepository;
        private IChefRepository _chefRepository;
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private bool _disposed;

        public UnitOfWork(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IDishRepository DishRepository
        {
            get { return _dishRepository ?? (_dishRepository = new DishRepository(_transaction)); }
        }

        public IChefRepository ChefRepository
        {
            get { return _chefRepository ?? (_chefRepository = new ChefRepository(_transaction)); }
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                ResetRepositories();
            }
        }

        private void ResetRepositories()
        {
            _dishRepository = null;
            _chefRepository = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }
    }
}
