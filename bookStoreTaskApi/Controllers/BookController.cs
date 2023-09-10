using bookStoreTaskApi.Models;
using Microsoft.AspNetCore.Mvc;


namespace bookStoreTaskApi.Controllers
{
    [Route ("api/v1/books")]
    [ApiController]
    public class BookController : ControllerBase 
    {
        private readonly List<Models.Book> _books = new()
        {
            new Book {Id = 1, Title = "Love of Christ", Author = "Gbile Akanni", Price = 20.45m, Quantity = 4},
            new Book {Id = 2, Title = "Kingdom of God", Author = "Myles Munroe", Price = 34.56m, Quantity = 6,},
        };

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBook()
        {
            return Ok(_books);
        }

        [HttpGet("(id)")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound("Book Not found");
            return Ok(book);
        }
    }
}
