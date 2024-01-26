﻿using Domain.Products.ProductAggregate.Validators;
using Domain.SeedWork;

namespace Domain.Products.ProductAggregate;

public sealed class Product : Entity, IAggregatedRoot
{
    public string Name { get; private set; }
    public Category Category { get; set; }

    public Product(string name, Category category, Guid id)
    {
        Id = id;
        Name = name;
        Category = category;

        if (Validator.IsValid(this, out var error) is false)
            throw new DomainException(error);
    }

    private static readonly IValidator<Product> Validator = new ProductValidator();
}