using Microsoft.EntityFrameworkCore;

namespace OrderService;

public class ApplicationDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}

public class Order
{
    public int Id { get; set; }
    public int Credits { get; set; }
    public int CustomerId { get; set; }
}