﻿using System.ComponentModel.DataAnnotations;

namespace TNMarketplace.Core.Entities
{
    public class Resource: Entity
    {
        [Key]
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public virtual Culture Culture { get; set; }
    }
}
