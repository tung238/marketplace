using System;
using System.Collections.Generic;
using System.Text;
using TNMarketplace.Core.Entities;
using TNMarketplace.Repository.Repositories;

namespace TNMarketplace.Service
{
    public interface IOrderService : IService<Order>
    {
    }

    public class OrderService : Service<Order>, IOrderService
    {
        public OrderService(IRepositoryAsync<Order> repository)
            : base(repository)
        {
        }
    }
}
