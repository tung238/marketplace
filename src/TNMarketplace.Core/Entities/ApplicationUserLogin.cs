using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TNMarketplace.Core.Infrastructure;

namespace TNMarketplace.Core.Entities
{
    public class ApplicationUserLogin: IdentityUserLogin<int>, IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}
