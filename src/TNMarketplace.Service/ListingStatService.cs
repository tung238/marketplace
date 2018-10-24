using System;
using System.Collections.Generic;
using System.Text;
using TNMarketplace.Core.Entities;
using TNMarketplace.Repository.Repositories;

namespace TNMarketplace.Service
{
    public interface IListingStatService : IService<ListingStat>
    {
    }

    public class ListingStatService : Service<ListingStat>, IListingStatService
    {
        public ListingStatService(IRepositoryAsync<ListingStat> repository)
            : base(repository)
        {
        }
    }
}
