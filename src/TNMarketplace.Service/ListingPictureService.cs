using System;
using System.Collections.Generic;
using System.Text;
using TNMarketplace.Core.Entities;
using TNMarketplace.Repository.Repositories;

namespace TNMarketplace.Service
{
    public interface IListingPictureService : IService<ListingPicture>
    {
    }

    public class ListingPictureService : Service<ListingPicture>, IListingPictureService
    {
        public ListingPictureService(IRepositoryAsync<ListingPicture> repository)
            : base(repository)
        {
        }
    }
}
