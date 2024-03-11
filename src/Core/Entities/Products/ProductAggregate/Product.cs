using Entities.Products.ProductAggregate.Validators;
using Entities.SeedWork;

namespace Entities.Products.ProductAggregate;

public sealed class Product : Entity, IAggregatedRoot
{
    public string Name { get; private set; }
    public Category Category { get; private set; }
    public decimal Price { get; private set; }
    public string Description { get; private set; }
    public List<Image> Images { get; private set; }

    public Product(Guid id, string name, Category category, decimal price, string description, List<Image> images)
    {
        Id = id;
        Name = name;
        Category = category;
        Price = price;
        Description = description;
        Images = images;

        if (Validator.IsValid(this, out var error) is false)
            throw new DomainException(error);
    }

    private static readonly IValidator<Product> Validator = new ProductValidator();

    // Required for EF
    public Product()
    {
    }
}