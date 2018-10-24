using System;
using System.Collections.Generic;
using System.Text;
using TNMarketplace.Core.Entities;
using TNMarketplace.Repository.Repositories;

namespace TNMarketplace.Service
{
    public interface IMessageParticipantService : IService<MessageParticipant>
    {
    }

    public class MessageParticipantService : Service<MessageParticipant>, IMessageParticipantService
    {
        public MessageParticipantService(IRepositoryAsync<MessageParticipant> repository)
            : base(repository)
        {
        }
    }
}
