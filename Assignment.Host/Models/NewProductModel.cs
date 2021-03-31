using FluentValidation;

namespace MVC_Assignment.Models
{
    public class NewProductModel
    {
        public string Name { get; set; }
        public int Price { get; set; }
    }

    public class NewProductValidator : AbstractValidator<NewProductModel>
    {
        public NewProductValidator()
        {
            RuleFor(_ => _.Name)
                .NotNull().NotEmpty().WithMessage("The name is required");

            RuleFor(_ => _.Price)
                .GreaterThan(0).WithMessage("The price must be a positive number");
        }
    }
}
