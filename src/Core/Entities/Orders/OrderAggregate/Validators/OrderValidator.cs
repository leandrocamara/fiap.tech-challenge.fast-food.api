using Entities.SeedWork;

namespace Entities.Orders.OrderAggregate.Validators
{
    internal sealed class OrderValidator : IValidator<Order>
    {
        public bool IsValid(Order order, out string error)
        {
            var rule = new Specifications<Order>(
                new IsOrderItensProvided());

            return rule.IsSatisfiedBy(order, out error);
        }
    }

    internal class IsOrderItensProvided : ISpecification<Order>
    {
        public bool IsSatisfiedBy(Order order, out string error)
        {
            error = "No product was chosen";
            return order.IsEmpty();
        }
    }
}
