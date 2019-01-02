using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities
{
    public class Area : Entity
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public string Slug { get; set; }
        public string Type { get; set; }
        public string NameWithType { get; set; }
        public int RegionId { get; set; }
        public string Path { get; set; }
        public string PathWithType { get; set; }
        public int Ordering { get; set; }
        public virtual Region Region { get; set; }
        public virtual ICollection<Listing> Listings { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
