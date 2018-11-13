using System;
using System.Collections.Generic;
using TNMarketplace.Core.Entities;

namespace TNMarketplace.Core.ViewModels
{
    public class SimpleListing
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public int ListingTypeID { get; set; }
        public string UserID { get; set; }
        public int RegionId { get; set; }
        public int AreaId { get; set; }
        public Nullable<double> Price { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public bool ShowPhone { get; set; }
        public bool Active { get; set; }
        public bool Enabled { get; set; }
        public bool ShowEmail { get; set; }
        public System.DateTime Expiration { get; set; }
        public string Location { get; set; }
        public Nullable<double> Latitude { get; set; }
        public Nullable<double> Longitude { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public virtual SimpleCategory Category { get; set; }
        public virtual SimpleRegion Region { get; set; }
        public virtual SimpleArea Area { get; set; }

        public virtual ICollection<ListingMeta> ListingMetas { get; set; }
        public virtual ICollection<ListingReview> ListingReviews { get; set; }
        public virtual SimpleListingType ListingType { get; set; }
        public virtual ICollection<ListingStat> ListingStats { get; set; }
    }
}