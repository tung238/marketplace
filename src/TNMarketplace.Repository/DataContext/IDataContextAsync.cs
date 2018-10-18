﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TNMarketplace.Repository.DataContext
{
    public interface IDataContextAsync : IDataContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync();
    }
}
