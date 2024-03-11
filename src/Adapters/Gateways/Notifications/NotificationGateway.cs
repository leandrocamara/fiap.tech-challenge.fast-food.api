using Application.Gateways;
using Entities.Orders.OrderAggregate;

namespace Adapters.Gateways.Notifications;

public class NotificationGateway(INotificationClient client) : INotificationGateway
{
    public void NotifyOrderPaymentUpdate(Order order) => client.NotifyOrderPaymentUpdate(order);

    public void NotifyOrderStatusUpdate(Order order) => client.NotifyOrderStatusUpdate(order);
}