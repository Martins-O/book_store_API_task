using bookStoreTaskApi.Services;

namespace bookStoreTaskApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int BookId {  get; set; }
        public int Quantity { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
    }
}
