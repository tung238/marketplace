using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TNMarketplace.Core.Infrastructure;

namespace TNMarketplace.Core.Entities
{
    public class ApplicationUserClaim : IdentityUserClaim<string>
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}
