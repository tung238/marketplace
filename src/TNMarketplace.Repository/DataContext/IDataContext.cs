using System;
using System.Collections.Generic;
using System.Text;
using TNMarketplace.Core.Infrastructure;

namespace TNMarketplace.Repository.DataContext
{
    public interface IDataContext : IDisposable
    {
        int SaveChanges();
        void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState;
        void SyncObjectsStatePostCommit();
    }
}
