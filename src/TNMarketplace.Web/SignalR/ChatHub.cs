using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace TNMarketplace.Web.SignalR
{
    public class Chat : Hub
    {
        public Task Send(string message)
        {
            return Clients.All.SendAsync("Send", message);
        }
    }
}