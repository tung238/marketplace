using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities
{
    public partial class CategoryStat : Entity
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public int Count { get; set; }
        public virtual Category Category { get; set; }
    }
}
