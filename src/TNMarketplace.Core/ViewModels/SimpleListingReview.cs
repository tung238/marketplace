using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.ViewModels
{
    public class SimpleListingReview
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public Nullable<int> ListingID { get; set; }
        public Nullable<int> OrderID { get; set; }
        public string UserFrom { get; set; }
        public string UserTo { get; set; }
        public bool Active { get; set; }
        public bool Enabled { get; set; }
        public bool Spam { get; set; }
        public SimpleUser AspNetUserFrom { get; set; }
        public SimpleUser AspNetUserTo { get; set; }
    }
}
