using Entities.SeedWork;

namespace Entities.Customers.CustomerAggregate.Validators;

internal sealed class EmailValidator : IValidator<Email>
{
    public bool IsValid(Email email, out string error)
    {
        var rule = new IsValidEmail();
        return rule.IsSatisfiedBy(email, out error);
    }
}

internal class IsValidEmail : ISpecification<Email>
{
    public bool IsSatisfiedBy(Email email, out string error)
    {
        error = "Invalid Email";

        var index = email.Value.IndexOf('@');

        return
            index > 0 &&
            index != email.Value.Length - 1 &&
            index == email.Value.LastIndexOf('@');
    }
}