using Microsoft.EntityFrameworkCore;

namespace bookStoreTaskApi.Models
{
    public class OrderDB : DbContext
    {
        public DbSet<Order> Books { get; set; }
        public OrderDB(DbContextOptions options) : base(options){ }
    }
}
