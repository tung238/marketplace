using System;
using System.Collections.Generic;
using System.Text;
using TNMarketplace.Core.Entities;
using TNMarketplace.Repository.Repositories;

namespace TNMarketplace.Service
{
    public interface IPictureService : IService<Picture>
    {
    }

    public class PictureService : Service<Picture>, IPictureService
    {
        public PictureService(IRepositoryAsync<Picture> repository)
            : base(repository)
        {
        }
    }
}
