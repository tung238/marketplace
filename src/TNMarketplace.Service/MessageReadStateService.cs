using System;
using System.Collections.Generic;
using System.Text;
using TNMarketplace.Core.Entities;
using TNMarketplace.Repository.Repositories;

namespace TNMarketplace.Service
{
    public interface IMessageReadStateService : IService<MessageReadState>
    {
    }

    public class MessageReadStateService : Service<MessageReadState>, IMessageReadStateService
    {
        public MessageReadStateService(IRepositoryAsync<MessageReadState> repository)
            : base(repository)
        {
        }
    }
}
