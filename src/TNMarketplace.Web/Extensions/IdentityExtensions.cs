using System;
using System.Security.Claims;
using AspNet.Security.OpenIdConnect.Primitives;

namespace TNMarketplace.Web.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var id = principal.FindFirst(OpenIdConnectConstants.Claims.Subject)?.Value;

            return id;
        }
    }
}
