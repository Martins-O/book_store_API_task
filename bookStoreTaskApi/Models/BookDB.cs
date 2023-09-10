using Microsoft.EntityFrameworkCore;

namespace bookStoreTaskApi.Models

{
    public class BookDB : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public BookDB(DbContextOptions contextOptions) : base(contextOptions) 
        { 
        }
    }
}
