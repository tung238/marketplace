using System;
using System.Collections.Generic;
using System.Text;
using TNMarketplace.Core.Entities;

namespace TNMarketplace.Core.ViewModels
{
    public class MetaFieldModel
    {
        public SimpleMetaField MetaField { get; set; }

        public List<SimpleCategory> Categories { get; set; }
    }
}
