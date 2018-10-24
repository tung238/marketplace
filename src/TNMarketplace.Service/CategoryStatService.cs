using System;
using System.Collections.Generic;
using System.Text;
using TNMarketplace.Core.Entities;
using TNMarketplace.Repository.Repositories;

namespace TNMarketplace.Service
{
    public interface ICategoryStatService : IService<CategoryStat>
    {
    }

    public class CategoryStatService : Service<CategoryStat>, ICategoryStatService
    {
        public CategoryStatService(IRepositoryAsync<CategoryStat> repository)
            : base(repository)
        {
        }
    }
}
