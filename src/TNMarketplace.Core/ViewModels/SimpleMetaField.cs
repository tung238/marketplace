using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.ViewModels
{
    public class SimpleMetaField
    {
        public SimpleMetaField()
        {
            this.ListingMetas = new List<SimpleListingMeta>();
            this.MetaCategories = new List<SimpleMetaCategory>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Placeholder { get; set; }
        public int ControlTypeID { get; set; }
        public string Options { get; set; }
        public bool Required { get; set; }
        public bool Searchable { get; set; }
        public Nullable<int> Ordering { get; set; }
        public virtual ICollection<SimpleListingMeta> ListingMetas { get; set; }
        public virtual ICollection<SimpleMetaCategory> MetaCategories { get; set; }
    }
}
