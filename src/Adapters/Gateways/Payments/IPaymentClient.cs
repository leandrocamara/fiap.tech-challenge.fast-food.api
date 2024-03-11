using Entities.Orders.OrderAggregate;

namespace Adapters.Gateways.Payments;

public interface IPaymentClient
{
    string GetQrCodeForPay(Order order);
}