using Application.Gateways;

namespace Application.UseCases.Customers.Validators;

public sealed class CustomerCreationValidator(ICustomerGateway customerGateway)
{
    public async Task Validate(CreateCustomerRequest request)
    {
        if (await IsCpfAlreadyUsed(request.Cpf))
            throw new ApplicationException("CPF already used");
    }

    private async Task<bool> IsCpfAlreadyUsed(string cpf) => await customerGateway.GetByCpf(cpf) != null;
}