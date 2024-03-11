using Entities.SeedWork;

namespace Entities.Products.ProductAggregate.Validators
{
    internal sealed class ImageValidator : IValidator<Image>
    {
        public bool IsValid(Image image, out string error)
        {
            var rule = new IsValidImage();
            return rule.IsSatisfiedBy(image, out error);
        }
    }

    internal class IsValidImage : ISpecification<Image>
    {
        public bool IsSatisfiedBy(Image image, out string error)
        {
            error = "Invalid Image";
            //return Path.Exists(image.Url);
            return true;

        }
    }
}