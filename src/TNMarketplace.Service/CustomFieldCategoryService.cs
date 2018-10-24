using System;
using System.Collections.Generic;
using System.Text;
using TNMarketplace.Core.Entities;
using TNMarketplace.Repository.Repositories;

namespace TNMarketplace.Service
{
    public interface ICustomFieldCategoryService : IService<MetaCategory>
    {
    }

    public class CustomFieldCategoryService : Service<MetaCategory>, ICustomFieldCategoryService
    {
        public CustomFieldCategoryService(IRepositoryAsync<MetaCategory> repository)
            : base(repository)
        {
        }
    }
}
