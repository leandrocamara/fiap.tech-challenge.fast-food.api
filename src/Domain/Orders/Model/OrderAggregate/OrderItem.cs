﻿using Domain.Products.Model.ProductAggregate;
using Domain.SeedWork;

namespace Domain.Orders.Model.OrderAggregate;

public class OrderItem : Entity
{
    public Order Order { get; private set; }
    public Guid OrderId { get; private set; }
    public Product Product { get; private set; }
    public Guid ProductId { get; private set; }
    public short Quantity { get; private set; }
    public decimal TotalPrice { get; private set; }

    public OrderItem(Product product, short quantity)
    {
        Id = Guid.NewGuid();
        Product = product;
        Quantity = quantity;
        TotalPrice = product.Price * quantity;
    }

    public void SetOrder(Order order)
    {
        Order = order;
        OrderId = order.Id;
    }

    // Required for EF
    private OrderItem()
    {
    }
}