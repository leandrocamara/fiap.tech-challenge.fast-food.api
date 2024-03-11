using Application.Gateways;
using Entities.Products.ProductAggregate;

namespace Adapters.Gateways.Products;

public class ProductGateway(IProductRepository repository) : IProductGateway
{
    public void Save(Product product) => repository.Add(product);

    public void Update(Product product) => repository.Update(product);

    public void Delete(Product product) => repository.Delete(product);

    public Product? GetById(Guid id) => repository.GetById(id);

    public Task<IEnumerable<Product>> GetByCategory(Category category) => repository.GetByCategory(category);
}