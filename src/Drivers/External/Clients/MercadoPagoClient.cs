using System.Globalization;
using Adapters.Gateways.Payments;
using Entities.Orders.OrderAggregate;

namespace External.Clients;

internal class MercadoPagoClient : IPaymentClient
{
    public string GetQrCodeForPay(Order order) => string.Format(
        $"OrderId={order.Id}:Valor={order.TotalPrice.ToString(CultureInfo.InvariantCulture)}:Tipo={nameof(MercadoPagoClient)}");
}