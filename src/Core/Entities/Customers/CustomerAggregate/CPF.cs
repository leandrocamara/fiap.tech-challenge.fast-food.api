using Entities.Customers.CustomerAggregate.Validators;
using Entities.SeedWork;

namespace Entities.Customers.CustomerAggregate;

public readonly struct Cpf
{
    public string Value { get; }

    public Cpf(string value)
    {
        Value = string.Concat(value.Where(char.IsDigit));
        Validate();
    }

    private void Validate()
    {
        if (Validator.IsValid(this, out var error) is false)
            throw new DomainException(error);
    }

    public override string ToString() => Value;

    public static implicit operator Cpf(string value) => new(value);

    public static implicit operator string(Cpf cpf) => cpf.Value;

    private static readonly IValidator<Cpf> Validator = new CpfValidator();
}