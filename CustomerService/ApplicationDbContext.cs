using Microsoft.EntityFrameworkCore;

namespace CustomerService;

public class ApplicationDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}

public class Customer
{
    public int Id { get; set; }
    public int Credits { get; set; }
}