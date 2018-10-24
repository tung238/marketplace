using System;
using System.Collections.Generic;
using System.Text;
using TNMarketplace.Core.Entities;
using TNMarketplace.Repository.Repositories;

namespace TNMarketplace.Service
{
    public interface IEmailTemplateService : IService<EmailTemplate>
    {
    }

    public class EmailTemplateService : Service<EmailTemplate>, IEmailTemplateService
    {
        public EmailTemplateService(IRepositoryAsync<EmailTemplate> repository)
            : base(repository)
        {
        }
    }
}
