using FluentValidation;

namespace MVC_Assignment.Models
{
    public class RegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterValidator : AbstractValidator<RegisterModel>
    {
        public RegisterValidator()
        {
            RuleFor(_ => _.FirstName)
                .NotEmpty().WithMessage("Please enter a valid name");
            RuleFor(_ => _.LastName)
                .NotEmpty().WithMessage("Please enter a valid name");
            RuleFor(_ => _.Email)
                .NotEmpty().EmailAddress().WithMessage("Not a valid email address");
            RuleFor(_ => _.Password)
                .NotEmpty().WithMessage("Please enter a valid password");
        }
    }
}
