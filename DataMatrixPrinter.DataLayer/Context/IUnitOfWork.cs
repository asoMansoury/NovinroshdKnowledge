using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DataMatrixPrinter.DataLayer.Context
{
    public interface IUnitOfWork
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveAllChanges();
        Task<int> SaveAllChangesAsync();
        void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class;
        void MarkAsAdded<TEntity>(TEntity entity) where TEntity : class;
        IList<T> GetRows<T>(string sql, params object[] parameters) where T : class;
        IEnumerable<TEntity> AddThisRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        void ForceDatabaseInitialize();
    }
}