using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNMarketplace.Core.Entities;

namespace TNMarketplace.Web.Models
{
    public class CategoryModel
    {
        public CategoryModel()
        {
            CategoryListingTypeID = new List<int>();
        }

        public Category Category { get; set; }

        public List<ListingType> ListingTypes { get; set; }

        public List<int> CategoryListingTypeID { get; set; }
    }
}
