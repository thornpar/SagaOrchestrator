using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

    public CustomerController(DbContextOptions<ApplicationDbContext> dbContextOptions)
    {
        _dbContextOptions = dbContextOptions;
    }

    [HttpPut]
    public async Task<string> DeductCredits(int customerId, int credits)
    {
        await using var ctx = new ApplicationDbContext(_dbContextOptions);
        var customer = await ctx.Customers.FirstAsync(e => e.Id == customerId);
        customer.Credits -= credits;
        await ctx.SaveChangesAsync();
        return await Task.Run(() => $"deducted {credits} credits");
    }
}