using System;
using System.Collections.Generic;
using System.Text;
using TNMarketplace.Core.Entities;

namespace TNMarketplace.Core.ViewModels
{
    public class MetaFieldModel
    {
        public MetaField MetaField { get; set; }

        public List<Category> Categories { get; set; }
    }
}
