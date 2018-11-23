using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using TNMarketplace.Repository;

namespace TNMarketplace.ImportTool
{
    public class FakeUserResolverService: UserResolverService
    {
        public FakeUserResolverService(IHttpContextAccessor contextAccessor):base(contextAccessor)
        {

        }

        public override string GetUser()
        {
            return null;
        }
    }
}
