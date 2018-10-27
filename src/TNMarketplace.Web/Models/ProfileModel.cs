using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNMarketplace.Core.Entities;

namespace TNMarketplace.Web.Models
{
    public class ProfileModel
    {
        public List<ListingItemModel> Listings { get; set; }

        public ApplicationUser User { get; set; }

        public List<ListingReview> ListingReviews { get; set; }
    }
}
