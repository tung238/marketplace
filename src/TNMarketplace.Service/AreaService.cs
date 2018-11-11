using System;
using System.Collections.Generic;
using System.Text;
using TNMarketplace.Core.Entities;
using TNMarketplace.Repository.Repositories;

namespace TNMarketplace.Service
{
    public interface IAreaService : IService<Area>
    {
    }

    public class AreaService : Service<Area>, IAreaService
    {
        public AreaService(IRepositoryAsync<Area> repository)
            : base(repository)
        {
        }
    }
}
