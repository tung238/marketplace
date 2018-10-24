using System;
using System.Collections.Generic;
using System.Text;
using TNMarketplace.Core.Entities;
using TNMarketplace.Repository.Repositories;

namespace TNMarketplace.Service
{
    public interface ICustomFieldService : IService<MetaField>
    {
    }

    public class CustomFieldService : Service<MetaField>, ICustomFieldService
    {
        public CustomFieldService(IRepositoryAsync<MetaField> repository)
            : base(repository)
        {
        }
    }
}
