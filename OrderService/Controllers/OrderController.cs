using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OrderService.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;
    private readonly ICustomerService _customerService;
    private readonly IPublishEndpoint _publishEndpoint;

    public OrderController(DbContextOptions<ApplicationDbContext> dbContextOptions, ICustomerService customerService, IPublishEndpoint publishEndpoint)
    {
        _dbContextOptions = dbContextOptions;
        _customerService = customerService;
        _publishEndpoint = publishEndpoint;
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
        await _publishEndpoint.Publish(new Message
        {
            Text = "From orderservice"
        });
        await dbContext.SaveChangesAsync();
        return "";
    }


}