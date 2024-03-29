﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities
{
    public partial class MetaField : Entity
    {
        public MetaField()
        {
            this.ListingMetas = new List<ListingMeta>();
            this.MetaCategories = new List<MetaCategory>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Placeholder { get; set; }
        public int ControlTypeID { get; set; }
        public string Options { get; set; }
        public bool Required { get; set; }
        public bool Searchable { get; set; }
        public Nullable<int> Ordering { get; set; }
        public virtual ICollection<ListingMeta> ListingMetas { get; set; }
        public virtual ICollection<MetaCategory> MetaCategories { get; set; }
    }
}
