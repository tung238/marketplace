using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities
{
    public partial class CategoryListingType : Entity
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public int ListingTypeID { get; set; }
        public virtual Category Category { get; set; }
        public virtual ListingType ListingType { get; set; }
    }
}
