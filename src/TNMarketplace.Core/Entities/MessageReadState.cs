﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities
{
    public partial class MessageReadState : Entity
    {
        public int ID { get; set; }
        public int MessageID { get; set; }
        public string UserID { get; set; }
        public Nullable<System.DateTime> ReadDate { get; set; }
        public virtual ApplicationUser AspNetUser { get; set; }
        public virtual Message Message { get; set; }
    }
}
