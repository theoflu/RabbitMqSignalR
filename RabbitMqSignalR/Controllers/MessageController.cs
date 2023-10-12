using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMqSignalR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RabbitMqSignalR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpPost()]
        public IActionResult Post([FromForm]Device device)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri("amqp://localhost");
            using IConnection connection = factory.CreateConnection();
            using IModel channel= connection.CreateModel();
            channel.QueueDeclare("messagequeue",false,false,false);
            string SData=JsonSerializer.Serialize(device);
            byte[] data = Encoding.UTF8.GetBytes(SData);
            channel.BasicPublish("", "messagequeue", body: data);



            return Ok();
        }
    }
}
