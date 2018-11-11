using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Repository.EfCore.JsonModel
{
    public class AreaJsonModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Slug { get; set; }
        public string NameWithType { get; set; }
        public string Path { get; set; }
        public string PathWithType { get; set; }
        public int ID { get; set; }
        public string ParentCode { get; set; }
    }
}
