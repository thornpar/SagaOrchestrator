using Refit;

namespace OrderService;

public interface ICustomerService
{
    [Put("/customer")]
    Task<string> DeductCredits(int customerId, int credits);

}