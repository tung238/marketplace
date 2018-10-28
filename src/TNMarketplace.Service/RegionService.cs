using System;
using System.Collections.Generic;
using System.Text;
using TNMarketplace.Core.Entities;
using TNMarketplace.Repository.Repositories;

namespace TNMarketplace.Service
{
    public interface IRegionService : IService<Region>
    {
    }

    public class RegionService : Service<Region>, IRegionService
    {
        public RegionService(IRepositoryAsync<Region> repository)
            : base(repository)
        {
        }
    }
}
