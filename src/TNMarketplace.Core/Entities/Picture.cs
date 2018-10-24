using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities
{
    public partial class Picture : Entity
    {
        public Picture()
        {
            this.ListingPictures = new List<ListingPicture>();
        }

        public int ID { get; set; }
        public string MimeType { get; set; }
        public string SeoFilename { get; set; }
        public virtual ICollection<ListingPicture> ListingPictures { get; set; }
    }
}
