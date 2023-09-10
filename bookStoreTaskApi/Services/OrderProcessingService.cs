using RabbitMQ.Client;

namespace bookStoreTaskApi.Services
{
    public class OrderProcessingService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public OrderProcessingService()
        {
            // Initialize RabbitMQ connection and channel
            var factory = new ConnectionFactory
            {
                HostName = "localhost", 
                Port = 5672,            
                UserName = "guest",     
                Password = "guest" 
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }
    }
}
