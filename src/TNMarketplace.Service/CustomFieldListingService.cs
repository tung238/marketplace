using System;
using System.Collections.Generic;
using System.Text;
using TNMarketplace.Core.Entities;
using TNMarketplace.Repository.Repositories;

namespace TNMarketplace.Service
{
    public interface ICustomFieldListingService : IService<ListingMeta>
    {
    }

    public class CustomFieldListingService : Service<ListingMeta>, ICustomFieldListingService
    {
        public CustomFieldListingService(IRepositoryAsync<ListingMeta> repository)
            : base(repository)
        {
        }
    }
}
