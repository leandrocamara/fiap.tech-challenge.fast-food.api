﻿using Domain.Orders.Model.OrderAggregate;

namespace Application.ACL.Payment
{
    public interface IPaymentGateway
    {
        /// <summary>
        /// Obter o qrcode para pagamento
        /// </summary>
        /// <param name="order">O pedido para gerar o qrcode</param>
        /// <returns>Em caso de sucesso, retorna o conteúdo a ser exibido no QrCode</returns>
        string GetQrCodeForPay(Order order);
    }
}
