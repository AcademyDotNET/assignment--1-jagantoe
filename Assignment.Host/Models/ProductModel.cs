using FluentValidation;

namespace MVC_Assignment.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }

    public class ProductValidator : AbstractValidator<ProductModel>
    {
        public ProductValidator()
        {
            RuleFor(_ => _.Name)
                .NotNull().NotEmpty().WithMessage("The name is required");

            RuleFor(_ => _.Price)
                .GreaterThan(0).WithMessage("The price must be a positive number");
        }
    }
}
