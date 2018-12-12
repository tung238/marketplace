using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.ViewModels
{
    public class CustomFieldUpdateModel
    {
        public int? ID { get; set; }
        public List<int> Categories { get; set; }
        public List<String> Options { get; set; }
        public int? ControlTypeID { get; set; }
        public String Name { get; set; }
        public bool Required { get; set; }
        public bool Searchable { get; set; }
        public String Placeholder { get; set; }
        public int Ordering { get; set; }
    }
}
