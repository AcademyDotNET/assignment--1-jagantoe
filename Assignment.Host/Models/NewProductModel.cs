using FluentValidation;

namespace MVC_Assignment.Models
{
    public class NewProductModel
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Registration { get; set; }
    }

    public class NewProductValidator : AbstractValidator<NewProductModel>
    {
        public NewProductValidator()
        {
            RuleFor(_ => _.Name)
                .NotNull().NotEmpty().WithMessage("The name is required");

            RuleFor(_ => _.Price)
                .GreaterThan(0).WithMessage("The price must be a positive number");

            RuleFor(_ => _.Registration)
                .Must(_ => _ != null && _.StartsWith("ABC") && _.Length == 8).WithMessage("Invalid registration");
        }
    }
}
