using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.ViewModels
{
    public class SimpleMetaCategory
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public int FieldID { get; set; }
        public virtual SimpleCategory Category { get; set; }
        public virtual SimpleMetaField MetaField { get; set; }
    }
}
