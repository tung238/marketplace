using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TNMarketplace.Repository.Infrastructure;

namespace TNMarketplace.Core.Entities
{
    public class ApplicationUserRole: IdentityUserRole<int>, IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}
