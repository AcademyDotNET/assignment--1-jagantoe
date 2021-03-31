using FluentValidation;

namespace MVC_Assignment.Models
{
    public class BuyProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }

    public class BuyProductValidator: AbstractValidator<BuyProductModel>
    {
        public BuyProductValidator()
        {
            RuleFor(_ => _.Quantity)
            .GreaterThan(0).WithMessage("The quantity must be a positive number");
        }
    }
}
