using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TNMarketplace.Web.Models
{
    public class FileUploadResult
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Url { get; set; }
        public string ThumbUrl { get; set; }
    }
}
