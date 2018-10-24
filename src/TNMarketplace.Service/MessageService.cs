using System;
using System.Collections.Generic;
using System.Text;
using TNMarketplace.Core.Entities;
using TNMarketplace.Repository.Repositories;

namespace TNMarketplace.Service
{
    public interface IMessageService : IService<Message>
    {
    }

    public class MessageService : Service<Message>, IMessageService
    {
        public MessageService(IRepositoryAsync<Message> repository)
            : base(repository)
        {
        }
    }
}
