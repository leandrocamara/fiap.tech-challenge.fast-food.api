using Entities.Customers.CustomerAggregate.Validators;
using Entities.SeedWork;

namespace Entities.Customers.CustomerAggregate;

public readonly struct Email
{
    public string Value { get; }

    public Email(string value)
    {
        Value = value;
        Validate();
    }

    private void Validate()
    {
        if (Validator.IsValid(this, out var error) is false)
            throw new DomainException(error);
    }

    public override string ToString() => Value;

    public static implicit operator Email(string value) => new(value);

    public static implicit operator string(Email email) => email.Value;

    private static readonly IValidator<Email> Validator = new EmailValidator();
}