using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities
{
    public class Region: Entity
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public string Slug { get; set; }
        public string Type { get; set; }
        public string NameWithType { get; set; }
        public string Code { get; set; }

        public virtual ICollection<Listing> Listings { get; set; }

    }
}
