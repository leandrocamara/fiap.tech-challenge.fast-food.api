using Adapters.Gateways.Notifications;
using Entities.Orders.OrderAggregate;

namespace External.Clients;

internal class NotificationClient : INotificationClient
{
    public void NotifyOrderPaymentUpdate(Order order)
    {
        //throw new NotImplementedException();
    }

    public void NotifyOrderStatusUpdate(Order order)
    {
        //throw new NotImplementedException();
    }
}