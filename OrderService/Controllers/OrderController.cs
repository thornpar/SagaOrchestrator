using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OrderService.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;
    private readonly ICustomerService _customerService;

    public OrderController(DbContextOptions<ApplicationDbContext> dbContextOptions, ICustomerService customerService)
    {
        _dbContextOptions = dbContextOptions;
        _customerService = customerService;
    }

    [HttpPost]
    public async Task<string> CreateOrder(int credits, int customerId)
    {
        await _customerService.DeductCredits(customerId, credits);
        
        await using var dbContext = new ApplicationDbContext(_dbContextOptions);
        await dbContext.Orders.AddAsync(new Order
        {
            Credits = credits,
            CustomerId = customerId
        });
        //BAM!
        await dbContext.SaveChangesAsync();
        return "";
    }


}