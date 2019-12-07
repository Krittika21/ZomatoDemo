using System;
using System.Collections.Generic;
using System.Text;

namespace ZomatoDemo.Core.Hubs
{
    public class Message
    {
        public string Type { get; set; }
        public string Payload { get; set; }
    }
}
