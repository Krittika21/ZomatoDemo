using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZomatoDemo.Core.Hubs
{
    public interface ITypedHubClient
    {
        Task BroadcastMessage(string type, string payload);
    }
}
