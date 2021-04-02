using FluentValidation;

namespace MVC_Assignment.Models
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginValidator : AbstractValidator<LoginModel>
    {
        public LoginValidator()
        {
            RuleFor(_ => _.Email)
                .NotEmpty().EmailAddress().WithMessage("Not a valid email address");
            RuleFor(_ => _.Password)
                .NotEmpty().WithMessage("Please enter a valid password");
        }
    }
}
