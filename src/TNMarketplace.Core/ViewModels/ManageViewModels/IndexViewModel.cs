using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TNMarketplace.Core.ViewModels.ManageViewModels
{
    public class IndexViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }
        public string Email { get; set; }
        public int? RegionId { get; set; }
        public int? AreaId { get; set; }
        public string Location { get; set; }
        public bool IsBroker { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }
}
