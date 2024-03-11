using Entities.Orders.OrderAggregate;

namespace Application.Gateways;

public interface INotificationGateway
{
    void NotifyOrderPaymentUpdate(Order order);
    void NotifyOrderStatusUpdate(Order order);
}