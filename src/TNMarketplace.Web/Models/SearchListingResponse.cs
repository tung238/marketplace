using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNMarketplace.Core.Entities;
using X.PagedList;

namespace TNMarketplace.Web.Models
{
    public class SearchListingResponse: SortViewModel
    {

        public SimpleRegion Region { get; set; }

        public string Location { get; set; }

        public List<MetaCategory> MetaCategories { get; set; }

        //public List<ListingItemModel> Listings { get; set; }

        public IPagedList<ListingItemModel> ListingsPageList { get; set; }
        public PagedListMetaData PagedListMetaData { get; set; }
        public List<SimpleCategory> Categories { get; set; }
        public List<SimpleCategory> BreadCrumb { get; set; }
        public List<SimpleListingType> ListingTypes { get; set; }
    }
}
