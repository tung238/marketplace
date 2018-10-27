using System.ComponentModel.DataAnnotations;

namespace TNMarketplace.Core.Entities
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUserPhoto : Entity
    {

        [Key]
        public int Id { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
