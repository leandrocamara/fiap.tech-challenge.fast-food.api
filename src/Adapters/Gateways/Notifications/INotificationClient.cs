using Entities.Orders.OrderAggregate;

namespace Adapters.Gateways.Notifications;

public interface INotificationClient
{
    void NotifyOrderPaymentUpdate(Order order);
    void NotifyOrderStatusUpdate(Order order);
}