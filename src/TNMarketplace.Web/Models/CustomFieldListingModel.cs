using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNMarketplace.Core.Entities;

namespace TNMarketplace.Web.Models
{
    public class CustomFieldListingModel
    {
        public List<MetaCategory> MetaCategories { get; set; }

        public int ListingID { get; set; }
    }
}
