using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNMarketplace.Core.Entities;
using TNMarketplace.Core.ViewModels;

namespace TNMarketplace.Web.Models
{
    public class ListingUpdateModel
    {
        public ListingUpdateModel()
        {
            CustomFields = new CustomFieldListingModel();
        }

        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int[] CategoryIds { get; set; }
        public int ListingTypeId { get; set; }
        public string UserID { get; set; }
        public int[] RegionIds { get; set; }
        public Nullable<double> Price { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public bool? ShowPhone { get; set; }
        public bool? Active { get; set; }
        public bool? Enabled { get; set; }
        public bool? ShowEmail { get; set; }
        public System.DateTime Expiration { get; set; }
        public string Location { get; set; }
        public Nullable<double> Latitude { get; set; }
        public Nullable<double> Longitude { get; set; }
        public System.DateTime Created { get; set; }
        public System.DateTime LastUpdated { get; set; }
        public List<PictureModel> Pictures { get; set; }
        public CustomFieldListingModel CustomFields { get; set; }
    }
}
