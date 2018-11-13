using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TNMarketplace.Core.ViewModels
{
    public class SimpleRegion
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public string Slug { get; set; }
        public string Type { get; set; }
        public string NameWithType { get; set; }
    }
}
