using Adapters.Controllers.Common;
using Application.UseCases.Orders;

namespace Adapters.Controllers;

public interface IWebhookController
{
    Task<Result> UpdateStatusPayment(UpdatePaymentOrderRequest request);
}

public class WebhookController(
    IPostUpdatePaymentOrderUseCase updatePaymentUseCase) : BaseController, IWebhookController
{
    public async Task<Result> UpdateStatusPayment(UpdatePaymentOrderRequest request)
    {
        try
        {
            var response = await Execute(() => updatePaymentUseCase.Execute(request));
            return Result.Success(response);
        }
        catch (ControllerException e)
        {
            return e.Result;
        }
    }
}