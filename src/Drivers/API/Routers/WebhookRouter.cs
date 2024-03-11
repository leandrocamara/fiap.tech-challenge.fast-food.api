using Adapters.Controllers;
using Application.UseCases.Orders;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Routers;

[ApiController]
[Route("api/webhook")]
public class WebhookRouter(IWebhookController controller) : BaseRouter
{
    [HttpPost("orders/payment")]
    [SwaggerResponse(StatusCodes.Status200OK, "WebHook a ser consumido pelo Mercado Pago (meio de pagamento QrCode) para informar atualizações de pagamento.", typeof(UpdatePaymentOrderResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Pedido não encontrado.")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erros não tratados pelo sistema, sendo retornado o erro específico no corpo da resposta.")]
    public async Task<IActionResult> UpdateStatusPayment(UpdatePaymentOrderRequest request)
    {
        var result = await controller.UpdateStatusPayment(request);
        return HttpResponse(result);
    }
}