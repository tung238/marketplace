using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TNMarketplace.Core.Infrastructure;
using System.Collections.Generic;

namespace TNMarketplace.Core.Entities
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<string>
    {
        public bool IsEnabled { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }
        [StringLength(250)]
        public string FirstName { get; set; }
        [StringLength(250)]
        public string LastName { get; set; }
        [Phone]
        public string Mobile { get; set; }

        public ApplicationUserPhoto ProfilePhoto { get; set; }

        public int? RegionId { get; set; }
        public int? AreaId { get; set; }
        public bool IsBroker { get; set; }
        public virtual Region Region { get; set; }
        public virtual Area Area { get; set; }
        public string Location { get; set; }

        [NotMapped]
        public string Name
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }
        [NotMapped]
        public ObjectState ObjectState { get; set; }

        public virtual ICollection<Listing> Listings { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<MessageParticipant> MessageParticipants { get; set; }
        public virtual ICollection<MessageReadState> MessageReadStates { get; set; }
        public virtual ICollection<Order> OrdersProvider { get; set; }
        public virtual ICollection<Order> OrdersReceiver { get; set; }
        public virtual ICollection<ListingReview> ListingReviewsUserFrom { get; set; }
        public virtual ICollection<ListingReview> ListingReviewsUserTo { get; set; }
    }
}
