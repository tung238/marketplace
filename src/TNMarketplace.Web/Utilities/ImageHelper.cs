using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TNMarketplace.Web.Utilities
{
    public class ImageHelper
    {
        public static string fileFormat = "00000000";
        private readonly IHostingEnvironment _environment;

        public ImageHelper(IHostingEnvironment environment)
        {
            this._environment = environment;
        }
        public string ImageVersion(string filePath)
        {
            DateTime lastWriteTime = File.GetLastWriteTime(Path.Combine(_environment.WebRootPath, filePath));

            // display version in dex format
            return string.Format("{0}?v={1:x}", Path.Combine(_environment.WebRootPath, filePath), lastWriteTime.Ticks);
        }

        public bool HasImage(int id)
        {
            var filePath = string.Format("/images/listing/{0}.jpg", id.ToString(fileFormat));

            return File.Exists(Path.Combine(_environment.WebRootPath, filePath));
        }

        public string GetListingImagePath(int id)
        {
            var filePath = string.Format("/images/listing/{0}.jpg", id.ToString(fileFormat));
            if (File.Exists(Path.Combine(_environment.WebRootPath, filePath)))
            {
                return ImageVersion(filePath);
            }
            else
            {
                return "http://placehold.it/500x300";
            }
        }

        public string GetUserProfileImagePath(string name)
        {
            var filePath = string.Format("~/images/profile/{0}.jpg", name);
            if (File.Exists(Path.Combine(_environment.WebRootPath, filePath)))
            {
                return ImageVersion(filePath);
            }
            else
            {
                return "http://www.gravatar.com/avatar/?d=mm";
            }
        }

        public string GetCommunityImagePath(string name, string format = "jpg", bool returnEmptyIfNotFound = false)
        {
            var filePath = string.Format("/images/community/{0}.{1}", name, format);
            if (File.Exists(Path.Combine(_environment.WebRootPath, filePath)))
            {
                return ImageVersion(filePath);
            }
            else if (returnEmptyIfNotFound)
            {
                return string.Empty;
            }
            else
            {
                return "http://placehold.it/500x300";
            }
        }
    }
}
