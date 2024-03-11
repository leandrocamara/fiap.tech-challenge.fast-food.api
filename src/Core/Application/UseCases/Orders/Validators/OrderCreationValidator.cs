using Application.Gateways;

namespace Application.UseCases.Orders.Validators;

public sealed class OrderCreationValidator(ICustomerGateway customerGateway, IProductGateway productGateway)
{

    public async Task Validate(CreateOrderRequest request)
    {
        if (!await CustomerExistsOrNotInformed(request.CustomerId))
            throw new ApplicationException("Customer not found");

        if (!await ProductsExists(request.OrderItems))
            throw new ApplicationException("Product informed not found");
    }

    private async Task<bool> CustomerExistsOrNotInformed(Guid? customerId)
    {
        if (customerId != null)
            return customerGateway.GetById(customerId.Value) != null;

        return true;
    }

    private async Task<bool> ProductsExists(IEnumerable<OrderItemRequest> orderItems)
    {
        var result = false;
        foreach (var item in orderItems)
            result = productGateway.GetById(item.ProductId) != null;

        return result;
    }
}