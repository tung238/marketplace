using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TNMarketplace.Web.Utilities
{
    public static class RequestHelper
    {
        public static string GetVisitorIP(this HttpRequest request)
        {
            //return request.ServerVariables["HTTP_CF_CONNECTING_IP"] == null ? request.ServerVariables["REMOTE_ADDR"] : request.ServerVariables["HTTP_CF_CONNECTING_IP"];
            return "";
        }

        public static string GetVisitorCountry(this HttpRequest request)
        {
            //return request.ServerVariables["HTTP_CF_IPCOUNTRY"];
            return "";
        }

        public static string GetProtocol(this HttpRequest request)
        {
            //return request.ServerVkariables["HTTP_CF_VISITOR"];
            return "";
        }
    }
}
