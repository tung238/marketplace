using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNMarketplace.Core.Entities;
using TNMarketplace.Core.ViewModels;

namespace TNMarketplace.Web.Models
{
    public class ListingItemModel
    {
        public List<SimpleListing> ListingsOther { get; set; }

        public SimpleListing ListingCurrent { get; set; }

        public string UrlPicture { get; set; }

        public List<PictureModel> Pictures { get; set; }

        public List<DateTime> DatesBooked { get; set; }

        public SimpleUser User { get; set; }

        public List<ListingReview> ListingReviews { get; set; }
    }
}
