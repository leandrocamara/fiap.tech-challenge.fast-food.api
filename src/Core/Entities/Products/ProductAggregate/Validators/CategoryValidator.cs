using Entities.SeedWork;

namespace Entities.Products.ProductAggregate.Validators
{
    internal sealed class CategoryValidator: IValidator<Category>
    {
        public bool IsValid(Category category, out string error)
        {
            var rule = new IsValidCategory();
            return rule.IsSatisfiedBy(category, out error);
        }
    }


    internal class IsValidCategory : ISpecification<Category>
    {
        public bool IsSatisfiedBy(Category category, out string error)
        {
            error = "Invalid Category - { Meal = 0,Side = 1,Drink = 2,Dessert = 3 } ";
            return Enum.IsDefined(typeof(Category.ECategory), category.Value);
        }
    }
}
