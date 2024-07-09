using FluentValidation;

namespace ChatApp.Application.Messages.CreateMessage
{
    internal sealed class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
    {
        public CreateMessageCommandValidator()
        {
            this.RuleFor(c => c.Content.Value)
                .NotEmpty()
                .WithMessage("Message content is required");
        }
    }
}
