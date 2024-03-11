using Entities.Products.ProductAggregate.Validators;
using Entities.SeedWork;

namespace Entities.Products.ProductAggregate
{
    public readonly struct Category
    {
        public ECategory Value { get; }

        public Category(ECategory value)
        {
            Value = value;
            Validate();
        }

        private void Validate()
        {
            if (Validator.IsValid(this, out var error) is false)
                throw new DomainException(error);
        }

        public override string ToString() => Value.ToString();

        // Conversão implícita de Category para int
        public static implicit operator int(Category category) => (int)category.Value;

        // Conversão explícita de int para Category
        public static implicit operator Category(int value) => new((ECategory)value);

        private static readonly IValidator<Category> Validator = new CategoryValidator();

        public enum ECategory :short
        {
            Meal = 0,
            Side = 1,
            Drink = 2,
            Dessert = 3
        }
    }
}
