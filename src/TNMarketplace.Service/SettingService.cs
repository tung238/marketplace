using System;
using System.Collections.Generic;
using System.Text;
using TNMarketplace.Core.Entities;
using TNMarketplace.Repository.Repositories;

namespace TNMarketplace.Service
{
    public interface ISettingService : IService<Setting>
    {
    }

    public class SettingService : Service<Setting>, ISettingService
    {
        public SettingService(IRepositoryAsync<Setting> repository)
            : base(repository)
        {
        }
    }
}
