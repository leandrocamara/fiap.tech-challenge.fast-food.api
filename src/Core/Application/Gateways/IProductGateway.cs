using Entities.Products.ProductAggregate;

namespace Application.Gateways;

public interface IProductGateway
{
    void Save(Product product);
    void Update(Product product);
    void Delete(Product product);
    Product? GetById(Guid id);
    Task<IEnumerable<Product>> GetByCategory(Category category);
}