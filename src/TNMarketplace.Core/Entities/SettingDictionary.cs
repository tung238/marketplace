using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities
{
    public partial class SettingDictionary : Entity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int SettingID { get; set; }
        public virtual Setting Setting { get; set; }
    }
}
