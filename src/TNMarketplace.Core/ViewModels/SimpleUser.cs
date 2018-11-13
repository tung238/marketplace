﻿using TNMarketplace.Core.Entities;

namespace TNMarketplace.Core.ViewModels
{
    public class SimpleUser
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public virtual ApplicationUserPhoto ProfilePhoto { get; set; }
    }
}