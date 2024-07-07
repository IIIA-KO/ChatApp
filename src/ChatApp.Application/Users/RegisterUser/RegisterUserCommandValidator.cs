using FluentValidation;

namespace ChatApp.Application.Users.RegisterUser
{
    internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            this.RuleFor(c => c.UserName.Value).NotEmpty().WithMessage("Username is required");
        }
    }
}
