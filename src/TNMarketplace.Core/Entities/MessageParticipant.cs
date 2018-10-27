using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities
{
    public partial class MessageParticipant : Entity
    {
        public int ID { get; set; }
        public int MessageThreadID { get; set; }
        public string UserID { get; set; }
        public virtual ApplicationUser AspNetUser { get; set; }
        public virtual MessageThread MessageThread { get; set; }
    }
}
