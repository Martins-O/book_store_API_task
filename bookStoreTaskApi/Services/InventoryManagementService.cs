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
            // Initialize RabbitMQ connection and channel
            var factory = new ConnectionFactory
            {
                HostName = "localhost", // Change to RabbitMQ server host if needed
                Port = 5672,            // Default RabbitMQ port
                UserName = "guest",     // Default RabbitMQ username
                Password = "guest"      // Default RabbitMQ password
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            // Declare the queue
            _channel.QueueDeclare(queue: "new_order", durable: false, exclusive: false, autoDelete: false, arguments: null);

            // Set up a consumer to process incoming orders
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                var jsonMessage = Encoding.UTF8.GetString(message);

                var order = JsonConvert.DeserializeObject<Order>(jsonMessage);
                var order = Order.FromJson(message); // Deserialize the order message

                // Update inventory based on the received order

                Console.WriteLine($"Received new order: {order}");
            };

            // Register the consumer with the queue
            _channel.BasicConsume(queue: "new_order", autoAck: true, consumer: consumer);
        }
    }
}
