using Microsoft.EntityFrameworkCore;
using StockService_AsyncAPI.Models;

namespace StockService_AsyncAPI.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }

    }
}
