using Application.Gateways;
using Application.UseCases.Orders.Validators;
using Entities.Customers.CustomerAggregate;
using Entities.Orders.OrderAggregate;
using Entities.SeedWork;
using QRCoder;

namespace Application.UseCases.Orders;

public interface ICreateOrderUseCase : IUseCase<CreateOrderRequest, CreateOrderResponse>;

public sealed class CreateOrderUseCase(
    IOrderGateway orderGateway,
    ICustomerGateway customerGateway,
    IProductGateway productGateway,
    IPaymentGateway paymentGateway) : ICreateOrderUseCase
{
    private readonly OrderCreationValidator _validator = new(customerGateway, productGateway);

    public async Task<CreateOrderResponse> Execute(CreateOrderRequest request)
    {
        try
        {
            await _validator.Validate(request);

            var orderItems = new List<OrderItem>();

            foreach (var item in request.OrderItems)
            {
                var product = productGateway.GetById(item.ProductId);
                orderItems.Add(new OrderItem(product, item.Quantity));
            }

            var customer = GetCustomer(request.CustomerId);
            var order = new Order(customer, orderItems, orderGateway.GetNextOrderNumber());

            order.SetQrCode(paymentGateway.GetQrCodeForPay(order));
            orderGateway.Save(order);

            return new CreateOrderResponse(order.Id, order.OrderNumber, order.Status.ToString(), GetBase64QrCodeImage(order.QrCodePayment));
        }
        catch (DomainException e)
        {
            throw new ApplicationException($"Failed to register the order. Error: {e.Message}", e);
        }
    }

    private Customer? GetCustomer(Guid? customerId)
    {
        if (customerId is null)
            return null;

        return customerGateway.GetById(customerId.Value);
    }

    private static string GetBase64QrCodeImage(string qrCodText)
    {
        QRCodeGenerator qrGenerator = new();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrCodText, QRCodeGenerator.ECCLevel.Q);
        PngByteQRCode qrCode = new(qrCodeData);
        return Convert.ToBase64String(qrCode.GetGraphic(20));
    }
}

public record CreateOrderRequest(IEnumerable<OrderItemRequest> OrderItems, Guid? CustomerId = null);

public record OrderItemRequest(Guid ProductId, short Quantity);

public record CreateOrderResponse(Guid Id, int Number, string Status, string? Base64PngQrCode);