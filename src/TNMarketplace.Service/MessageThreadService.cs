using System;
using System.Collections.Generic;
using System.Text;
using TNMarketplace.Core.Entities;
using TNMarketplace.Repository.Repositories;

namespace TNMarketplace.Service
{
    public interface IMessageThreadService : IService<MessageThread>
    {
    }

    public class MessageThreadService : Service<MessageThread>, IMessageThreadService
    {
        public MessageThreadService(IRepositoryAsync<MessageThread> repository)
            : base(repository)
        {
        }
    }
}
