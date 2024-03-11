using Application.Gateways;
using Entities.Orders.OrderAggregate;

namespace Adapters.Gateways.Payments;

public class PaymentGateway(IPaymentClient client) : IPaymentGateway
{
    public string GetQrCodeForPay(Order order) => client.GetQrCodeForPay(order);
}