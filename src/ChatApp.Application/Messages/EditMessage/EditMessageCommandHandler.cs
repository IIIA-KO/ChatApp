using ChatApp.Application.Abstraction.Messaging;
using ChatApp.Domain.Abstraction;
using ChatApp.Domain.Chats;
using ChatApp.Domain.Messages;
using ChatApp.Domain.UserChats;
using ChatApp.Domain.Users;

namespace ChatApp.Application.Messages.EditMessage
{
    internal sealed class EditMessageCommandHandler : ICommandHandler<EditMessageCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserChatRepository _userChatRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EditMessageCommandHandler(
            IUserRepository userRepository,
            IUserChatRepository userChatRepository,
            IMessageRepository messageRepository,
            IUnitOfWork unitOfWork
        )
        {
            this._userRepository = userRepository;
            this._userChatRepository = userChatRepository;
            this._messageRepository = messageRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            EditMessageCommand request,
            CancellationToken cancellationToken
        )
        {
            User? user = await this._userRepository.GetByIdAsync(
                request.CreatorId,
                cancellationToken
            );
            if (user is null)
            {
                return Result.Failure(UserErrors.NotFound);
            }

            Message? message = await this._messageRepository.GetByIdAsync(
                request.MessageId,
                cancellationToken
            );
            if (message is null)
            {
                return Result.Failure(MessageErrors.NotFound);
            }

            if (message.UserId != user.Id)
            {
                return Result.Failure(MessageErrors.NotAuthorized);
            }

            if (
                !await this._userChatRepository.IsUserConnectedToChat(
                    user.Id,
                    message.ChatId,
                    cancellationToken
                )
            )
            {
                return Result.Failure(ChatErrors.NotConnected);
            }

            message.Content = request.Content;

            await this._unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(message);
        }
    }
}
