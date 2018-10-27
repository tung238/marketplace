using System;
using System.Collections.Generic;
using System.Text;

namespace TNMarketplace.Core.Entities
{
    public partial class Message : Entity
    {
        public Message()
        {
            this.MessageReadStates = new List<MessageReadState>();
        }

        public int ID { get; set; }
        public int MessageThreadID { get; set; }
        public string Body { get; set; }
        public string UserFrom { get; set; }
        public System.DateTime Created { get; set; }
        public System.DateTime LastUpdated { get; set; }
        public virtual ApplicationUser AspNetUser { get; set; }
        public virtual MessageThread MessageThread { get; set; }
        public virtual ICollection<MessageReadState> MessageReadStates { get; set; }
    }
}
