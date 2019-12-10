using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using ZomatoDemo.Core.Hubs;

namespace ZomatoDemo.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : Controller
    {
        //private readonly IHubContext<NotifyHub, ITypedHubClient> _hubContext;

        public MessageController()
        {

        }
    }
}
