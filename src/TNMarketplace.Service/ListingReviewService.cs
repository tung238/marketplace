using System;
using System.Collections.Generic;
using System.Text;
using TNMarketplace.Core.Entities;
using TNMarketplace.Repository.Repositories;

namespace TNMarketplace.Service
{
    public interface IListingReviewService : IService<ListingReview>
    {
    }

    public class ListingReviewService : Service<ListingReview>, IListingReviewService
    {
        public ListingReviewService(IRepositoryAsync<ListingReview> repository)
            : base(repository)
        {
        }
    }
}
