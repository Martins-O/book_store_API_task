using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using bookStoreTaskApi.Models;
using System.Text;
using Newtonsoft.Json;

namespace bookStoreTaskApi.Services
{
    public class InventoryManagementService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public InventoryManagementService()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost", 
                Port = 5672,            
                UserName = "guest",     
                Password = "guest"      
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "new_order", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());

                try
                {
                    var order = JsonConvert.DeserializeObject<Order>(message);

                    Console.WriteLine($"Received new order: {order}");
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Error deserializing order JSON: {ex.Message}");
                }
            };

            _channel.BasicConsume(queue: "new_order", autoAck: true, consumer: consumer);
        }
    }
}
