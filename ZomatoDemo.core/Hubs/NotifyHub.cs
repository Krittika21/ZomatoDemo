using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ZomatoDemo.Core.Hubs
{
    public class NotifyHub : Hub
    {
        public static string adminConnectionID;

        public void AdminCheck()
        {
            adminConnectionID = this.Context.ConnectionId;
        }

        public void SendNotification(string type, string payload)
        {
            if (adminConnectionID!=null)
            {
                Clients.Client(adminConnectionID).SendAsync("SendMessage", type, payload);
            }
            
        }

        public void RemoveConnection()
        {
            adminConnectionID = null;
        }
    }
}
