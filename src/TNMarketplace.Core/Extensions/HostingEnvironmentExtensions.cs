using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace TNMarketplace.Core
{
    public static class HostingEnvironmentExtensions
    {
        public static string[] GetTranslationFile(this IHostingEnvironment hostingEnvironment)
        {
            return File.ReadAllLines(Path.Combine(hostingEnvironment.ContentRootPath, "translations.csv"));
        }

    }
}
