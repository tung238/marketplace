using System;
using System.Collections.Generic;
using System.Text;
using TNMarketplace.Core.Entities;
using TNMarketplace.Repository.Repositories;

namespace TNMarketplace.Service
{
    public interface IListingTypeService : IService<ListingType>
    {
    }

    public class ListingTypeService : Service<ListingType>, IListingTypeService
    {
        public ListingTypeService(IRepositoryAsync<ListingType> repository)
            : base(repository)
        {
        }
    }
}
