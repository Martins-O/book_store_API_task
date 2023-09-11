using bookStoreTaskApi.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace bookStoreTaskApi.Services
{

    public enum OrderStatus
    {
        Pending,    
        Processing, 
        Shipped,   
        Completed,  
        Canceled
    }
    public class OrderProcessingService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly List<Book> _book;

        public OrderProcessingService()
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
            _book = new List<Book>();
        }

        public void PlaceOrder(Order order)
        {
            if (!IsOrderValid(order))
            {
                throw new InvalidOperationException("Invalid order.");
            }

            try
            {
                decimal totalPrice = CalculateTotalPrice(order);

                bool isStockAvailable = CheckStockAvailability(order);

                if (!isStockAvailable)
                {
                    throw new InvalidOperationException("Out of stock.");
                }

                order.Status = OrderStatus.Processing;

                SaveOrderToDatabase(order);

                var orderJson = JsonConvert.SerializeObject(order);

                var messageBody = Encoding.UTF8.GetBytes(orderJson);
                _channel.BasicPublish(exchange: "orders", routingKey: "new_order", body: messageBody);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing order: {ex.Message}");
            }
        }

        private bool IsOrderValid(Order order)
        {
            if (order.Quantity <= 0)
            {
                return false;
            }
            return true;
        }

        private decimal CalculateTotalPrice(Order order)
        {
            decimal totalPrice = 0;

            foreach (var item in order.OrderItems)
            {
                var book = _book.FirstOrDefault(b => b.Id == item.BookId);
                if (book != null)
                {
                    totalPrice += book.Price * item.Quantity;
                }
            }

            return totalPrice;
        }

        private bool CheckStockAvailability(Order order)
        {
            foreach (var item in order.OrderItems)
            {
                var book = _book.FirstOrDefault(b => b.Id == item.BookId);
                if (book == null || book.Quantity < item.Quantity)
                {
                    return false; // Out of stock
                }
            }

            return true;
        }

        private void SaveOrderToDatabase(Order order)
        {
            List<Order> _orders = new();
            _orders.Add(order);
        }
    }
}
