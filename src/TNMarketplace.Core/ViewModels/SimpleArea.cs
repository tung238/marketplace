using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.ViewModels
{
    public class SimpleArea
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public string Slug { get; set; }
        public string Type { get; set; }
        public string NameWithType { get; set; }
        public string PathWithType { get; set; }
    }
}
