using Microsoft.AspNetCore.SignalR.Client;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMqSignalR.Model;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmailSenderExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HubConnection hubConnection = new HubConnectionBuilder().WithUrl("https://localhost:5001/messageHub").Build();
            await  hubConnection.StartAsync();
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri("amqp://localhost");
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();
            channel.QueueDeclare("messagequeue", false, false, false);

            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume("messagequeue", true,consumer);
            consumer.Received +=  async(s, e)=>{
                string sdata = Encoding.UTF8.GetString(e.Body.Span);
                Device device=JsonSerializer.Deserialize<Device>(sdata);
                EmailSender.Send(device.Email,device.Message);
                Console.WriteLine("Email gönderildi.");
              await  hubConnection.InvokeAsync("SendMessageAsync","Email gönderildi",device.ConnectionId);
            };
            Console.Read();
        }
    }
}
