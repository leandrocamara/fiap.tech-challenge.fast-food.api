namespace Entities.Orders.OrderAggregate;

public readonly struct OrderStatus
{
    private EOrderStatus Value { get; }

    public static OrderStatus PaymentPending() => new(EOrderStatus.PaymentPending);
    public static OrderStatus PaymentRefused() => new(EOrderStatus.PaymentRefused);
    public static OrderStatus Received() => new(EOrderStatus.Received);
    public static OrderStatus Preparing() => new(EOrderStatus.Preparing);
    public static OrderStatus Ready() => new(EOrderStatus.Ready);
    public static OrderStatus Completed() => new(EOrderStatus.Completed);

    public static implicit operator short(OrderStatus status) => (short)status.Value;
    public static implicit operator OrderStatus(short value) => new((EOrderStatus)value);
    public static implicit operator string(OrderStatus status) => status.ToString();

    public override string ToString() => Value.ToString();

    private OrderStatus(EOrderStatus status) => Value = status;

    private enum EOrderStatus : short
    {
        PaymentPending = 0,
        PaymentRefused = 1,
        Received = 2,
        Preparing = 3,
        Ready = 4,
        Completed = 5
    }
}