using Entities.SeedWork;

namespace Entities.Customers.CustomerAggregate.Validators;

internal sealed class CustomerValidator : IValidator<Customer>
{
    public bool IsValid(Customer customer, out string error)
    {
        var rule = new Specifications<Customer>(
            new IsCustomerNameProvided(),
            new IsCustomerCpfProvided(),
            new IsCustomerEmailProvided());

        return rule.IsSatisfiedBy(customer, out error);
    }
}

internal class IsCustomerNameProvided : ISpecification<Customer>
{
    public bool IsSatisfiedBy(Customer customer, out string error)
    {
        error = "Name not provided";
        return string.IsNullOrWhiteSpace(customer.Name) is false;
    }
}

internal class IsCustomerCpfProvided : ISpecification<Customer>
{
    public bool IsSatisfiedBy(Customer customer, out string error)
    {
        error = "CPF not provided";
        return string.IsNullOrWhiteSpace(customer.Cpf) is false;
    }
}

internal class IsCustomerEmailProvided : ISpecification<Customer>
{
    public bool IsSatisfiedBy(Customer customer, out string error)
    {
        error = "Email not provided";
        return string.IsNullOrWhiteSpace(customer.Email) is false;
    }
}