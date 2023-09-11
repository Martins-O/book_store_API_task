using bookStoreTaskApi.Models;
using bookStoreTaskApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace bookStoreTaskApi.Controllers 
{
    [Route("api/v1/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly List<Order> _orders = new();
        private readonly List<Book> _book = new();

        private readonly OrderProcessingService _orderProcessingService;

        public OrderController(OrderProcessingService orderProcessingService)
        {
            _orderProcessingService = orderProcessingService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Order[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Order[]), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Order[]), StatusCodes.Status404NotFound)]
        public IActionResult PlaceOrder(Order order)
        {
            var book = _book.FirstOrDefault(b => b.Id == order.Id);
            if (book == null)
            {
                return NotFound("Book Not Found");
            }

            if(book.Quantity < order.Quantity)
            {
                return BadRequest($"Book '(book.Title)' is out of stock. ");
            }

            var orderId = _orders.Count + 1;
            order.Id = orderId;

            book.Quantity -= order.Quantity;

            _orders.Add(order);

            _orderProcessingService.PlaceOrder(order);

            return CreatedAtAction(nameof(GetOrderStatus), new { id = order.Id }, order);
        }

        [HttpGet("{id}")]
        public IEnumerable<ActionResult<Order>> GetOrderStatus(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
               yield return NotFound();

            yield return Ok(order);
        }
    }
}
