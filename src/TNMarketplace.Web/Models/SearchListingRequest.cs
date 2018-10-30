using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNMarketplace.Core.Entities;
using X.PagedList;

namespace TNMarketplace.Web.Models
{
    public class SearchListingRequest : SortViewModel
    {
        public List<int> ListingTypeID { get; set; }

        public List<String> UrlSegments { get; set; }

        public string SearchText { get; set; }

        public string Location { get; set; }

        public bool PhotoOnly { get; set; }

        public double? PriceFrom { get; set; }

        public double? PriceTo { get; set; }
    }
}
