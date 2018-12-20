using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNMarketplace.Core.Entities;
using TNMarketplace.Core.ViewModels;

namespace TNMarketplace.Web.Models
{
    public class ProfileModel
    {
        public List<ListingItemModel> Listings { get; set; }

        public SimpleUser User { get; set; }

        public List<SimpleListingReview> ListingReviews { get; set; }
    }
}
