using System;
using System.Collections.Generic;
using System.Text;
using TNMarketplace.Core.Entities;
using TNMarketplace.Repository.Repositories;

namespace TNMarketplace.Service
{
    public interface IContentPageService : IService<ContentPage>
    {
    }

    public class ContentPageService : Service<ContentPage>, IContentPageService
    {
        public ContentPageService(IRepositoryAsync<ContentPage> repository)
            : base(repository)
        {
        }
    }
}
