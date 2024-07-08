using FluentValidation;

namespace ChatApp.Application.Chats.CreateChat
{
    internal sealed class CreateChatCommandValidator : AbstractValidator<CreateChatCommand>
    {
        public CreateChatCommandValidator()
        {
            this.RuleFor(c => c.ChatName.Value).NotEmpty().WithMessage("Chat name is required");
        }
    }
}
