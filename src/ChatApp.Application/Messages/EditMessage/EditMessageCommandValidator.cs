using FluentValidation;

namespace ChatApp.Application.Messages.EditMessage
{
    internal sealed class EditMessageCommandValidator : AbstractValidator<EditMessageCommand>
    {
        public EditMessageCommandValidator()
        {
            this.RuleFor(c => c.Content.Value)
                .NotEmpty()
                .WithMessage("Message content is required");
        }
    }
}
