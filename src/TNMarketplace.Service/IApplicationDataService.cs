using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TNMarketplace.Service
{
    public interface IApplicationDataService
    {
        Task<object> GetApplicationData(HttpContext context);

        void RemoveCachedApplicationData();
    }
}
