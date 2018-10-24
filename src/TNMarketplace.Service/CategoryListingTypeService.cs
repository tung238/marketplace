using System;
using System.Collections.Generic;
using System.Text;
using TNMarketplace.Core.Entities;
using TNMarketplace.Repository.Repositories;

namespace TNMarketplace.Service
{
    public interface ICategoryListingTypeService : IService<CategoryListingType>
    {

    }

    public class CategoryListingTypeService : Service<CategoryListingType>, ICategoryListingTypeService
    {
        public CategoryListingTypeService(IRepositoryAsync<CategoryListingType> repository)
            : base(repository)
        {
        }
    }
}
