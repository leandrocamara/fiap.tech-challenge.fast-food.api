﻿using Domain.SeedWork;

namespace Domain.Product.ProductAggregate.Validators;

internal sealed class ProductValidator : IValidator<Product>
{
    public bool IsValid(Product product, out string error)
    {
        var rule = new Specifications<Product>(
            new IsProductNameProvided(),
            new IsProductPriceProvided(),
            new IsProductDescriptionProvided());

        return rule.IsSatisfiedBy(product, out error);
    }
}

internal class IsProductNameProvided : ISpecification<Product>
{
    public bool IsSatisfiedBy(Product product, out string error)
    {
        error = "Name not provided";
        return string.IsNullOrWhiteSpace(product.Name) is false;
    }
}

internal class IsProductPriceProvided : ISpecification<Product>
{
    public bool IsSatisfiedBy(Product product, out string error)
    {
        error = "Price not provided";
        return product.Price > 0;
    }
}

internal class IsProductDescriptionProvided : ISpecification<Product>
{
    public bool IsSatisfiedBy(Product product, out string error)
    {
        error = "Description not provided";
        return string.IsNullOrWhiteSpace(product.Description) is false;
    }
}

