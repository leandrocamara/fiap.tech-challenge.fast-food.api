using Entities.Products.ProductAggregate.Validators;
using Entities.SeedWork;

namespace Entities.Products.ProductAggregate;

public struct Image
{
    public string Url { get; set; }

    public Image(string value)
    {
        Url = value;
        Validate();
    }

    private void Validate()
    {
        if (Validator.IsValid(this, out var error) is false)
            throw new DomainException(error);
    }

    public override string ToString() => Url;

    //public static implicit operator Image(string value) => new(value);

    //public static implicit operator string(Image image) => image.Url;

    public static List<Image> ConvertToImages(List<string> values) => values.ConvertAll(x => new Image(x));
    public static List<string> ConvertToStrings(List<Image> images) => images.Select(it => it.Url).ToList();

    private static readonly IValidator<Image> Validator = new ImageValidator();
}


