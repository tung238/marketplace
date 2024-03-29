﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities
{
    public partial class Category : Entity
    {
        public Category()
        {
            //this.CategoryListingTypes = new List<CategoryListingType>();
            this.CategoryStats = new List<CategoryStat>();
            this.Listings = new List<Listing>();
            this.MetaCategories = new List<MetaCategory>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string IconClass { get; set; }
        public string Description { get; set; }
        public int Parent { get; set; }
        public bool Enabled { get; set; }
        public int Ordering { get; set; }
        public double? MaxPrice { get; set; }
        public string PriceRanges { get; set; }
        //public virtual ICollection<CategoryListingType> CategoryListingTypes { get; set; }
        public virtual ICollection<CategoryStat> CategoryStats { get; set; }
        public virtual ICollection<Listing> Listings { get; set; }
        public virtual ICollection<MetaCategory> MetaCategories { get; set; }
    }
}
