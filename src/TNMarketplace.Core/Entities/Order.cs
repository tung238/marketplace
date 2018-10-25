﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities
{
    public partial class Order : Entity
    {
        public Order()
        {
            this.ListingReviews = new List<ListingReview>();
        }

        public int ID { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public int ListingID { get; set; }
        public int ListingTypeID { get; set; }
        public int Status { get; set; }
        public Nullable<double> Quantity { get; set; }
        public Nullable<double> Price { get; set; }
        public string Currency { get; set; }
        public Nullable<double> ApplicationFee { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
        public int UserProvider { get; set; }
        public int UserReceiver { get; set; }
        public string PaymentPlugin { get; set; }
        public System.DateTime Created { get; set; }
        public System.DateTime Modified { get; set; }
        public virtual ApplicationUser AspNetUserProvider { get; set; }
        public virtual ApplicationUser AspNetUserReceiver { get; set; }
        public virtual ICollection<ListingReview> ListingReviews { get; set; }
        public virtual Listing Listing { get; set; }
    }
}
