using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using TNMarketplace.Repository.Infrastructure;

namespace TNMarketplace.Core.Entities
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationRole : IdentityRole<int>, IObjectState
    {
        [StringLength(250)]
        public string Description { get; set; }
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}
