using Entities.SeedWork;

namespace Entities.Customers.CustomerAggregate.Validators;

internal sealed class CpfValidator : IValidator<Cpf>
{
    public bool IsValid(Cpf email, out string error)
    {
        var rule = new IsValidCpf();
        return rule.IsSatisfiedBy(email, out error);
    }
}

internal class IsValidCpf : ISpecification<Cpf>
{
    public bool IsSatisfiedBy(Cpf cpf, out string error)
    {
        error = "Invalid CPF";

        var position = 0;
        var totalDigit1 = 0;
        var totalDigit2 = 0;
        var verifyingDigit1 = 0;
        var verifyingDigit2 = 0;

        var identicalDigits = true;
        var lastDigit = -1;

        foreach (var digit in from c in cpf.Value where char.IsDigit(c) select c - '0')
        {
            if (position != 0 && lastDigit != digit)
            {
                identicalDigits = false;
            }

            lastDigit = digit;
            switch (position)
            {
                case < 9:
                    totalDigit1 += digit * (10 - position);
                    totalDigit2 += digit * (11 - position);
                    break;
                case 9:
                    verifyingDigit1 = digit;
                    break;
                case 10:
                    verifyingDigit2 = digit;
                    break;
            }

            position++;
        }

        if (position > 11)
            return false;

        if (identicalDigits)
            return false;

        var digit1 = totalDigit1 % 11;
        digit1 = digit1 < 2
            ? 0
            : 11 - digit1;

        if (verifyingDigit1 != digit1)
            return false;

        totalDigit2 += digit1 * 2;
        var digit2 = totalDigit2 % 11;
        digit2 = digit2 < 2
            ? 0
            : 11 - digit2;

        return verifyingDigit2 == digit2;
    }
}