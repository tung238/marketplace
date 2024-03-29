﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities
{
    public partial class MessageThread : Entity
    {
        public MessageThread()
        {
            this.Messages = new List<Message>();
            this.MessageParticipants = new List<MessageParticipant>();
        }

        public int ID { get; set; }
        public string Subject { get; set; }
        public Nullable<int> ListingID { get; set; }
        public virtual Listing Listing { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<MessageParticipant> MessageParticipants { get; set; }
    }
}
