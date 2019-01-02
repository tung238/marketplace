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
        public int Ordering { get; set; }
        public virtual ICollection<Area> Areas { get; set; }
        public virtual ICollection<Listing> Listings { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
