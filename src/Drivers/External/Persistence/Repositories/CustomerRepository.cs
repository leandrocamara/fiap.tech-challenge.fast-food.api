using Adapters.Gateways.Customers;
using Entities.Customers.CustomerAggregate;
using Microsoft.EntityFrameworkCore;

namespace External.Persistence.Repositories;

public sealed class CustomerRepository(FastFoodContext context) : BaseRepository<Customer>(context), ICustomerRepository
{ 
    public Task<Customer?> GetByCpf(Cpf cpf) =>
        context.Customers.FirstOrDefaultAsync(customer => customer.Cpf.Equals(cpf));
}