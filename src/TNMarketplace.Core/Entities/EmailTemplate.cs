using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities
{
    public partial class EmailTemplate : Entity
    {
        public int ID { get; set; }
        public string Slug { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool SendCopy { get; set; }
    }
}
