using Application.Gateways;
using Entities.SeedWork;

namespace Application.UseCases.Orders;

public interface IPostUpdatePaymentOrderUseCase : IUseCase<UpdatePaymentOrderRequest, UpdatePaymentOrderResponse>;

public sealed class PostUpdatePaymentOrderUseCase(IOrderGateway orderGateway, INotificationGateway notifyGateway)
    : IPostUpdatePaymentOrderUseCase
{
    public Task<UpdatePaymentOrderResponse> Execute(UpdatePaymentOrderRequest request)
    {
        try
        {
            var order = orderGateway.GetById(request.Id);

            if (order == null)
                throw new ApplicationException("Order not found");

            order.UpdatePaymentStatus(request.PaymentSucceeded);
            orderGateway.Update(order);

            notifyGateway.NotifyOrderPaymentUpdate(order);

            return Task.FromResult(new UpdatePaymentOrderResponse(order.Id, order.OrderNumber, order.Status));
        }
        catch (DomainException e)
        {
            throw new ApplicationException($"Failed to recover product. Error: {e.Message}", e);
        }
    }
}

public record UpdatePaymentOrderRequest(Guid Id, bool PaymentSucceeded);

public record UpdatePaymentOrderResponse(Guid Id, int Number, string Status);