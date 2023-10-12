using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMqSignalR.Model
{
    public class Device
    {
        public string ConnectionId { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
    }
}
