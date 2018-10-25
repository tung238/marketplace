using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities
{
    public partial class ListingReview : Entity
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public Nullable<int> ListingID { get; set; }
        public Nullable<int> OrderID { get; set; }
        public int UserFrom { get; set; }
        public int UserTo { get; set; }
        public bool Active { get; set; }
        public bool Enabled { get; set; }
        public bool Spam { get; set; }
        public System.DateTime Created { get; set; }
        public virtual ApplicationUser AspNetUserFrom { get; set; }
        public virtual ApplicationUser AspNetUserTo { get; set; }
        public virtual Listing Listing { get; set; }
        public virtual Order Order { get; set; }
    }
}
