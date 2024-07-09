using ChatApp.Application.Abstraction.Messaging;
using ChatApp.Domain.Abstraction;
using ChatApp.Domain.Chats;
using ChatApp.Domain.Messages;
using ChatApp.Domain.UserChats;
using ChatApp.Domain.Users;

namespace ChatApp.Application.Messages.CreateMessage
{
    internal sealed class CreateMessageCommandHandler : ICommandHandler<CreateMessageCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IChatRepository _chatRepository;
        private readonly IUserChatRepository _userChatRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateMessageCommandHandler(
            IUserRepository userRepository,
            IChatRepository chatRepository,
            IUserChatRepository userChatRepository,
            IMessageRepository messageRepository,
            IUnitOfWork unitOfWork
        )
        {
            this._userRepository = userRepository;
            this._chatRepository = chatRepository;
            this._userChatRepository = userChatRepository;
            this._messageRepository = messageRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            CreateMessageCommand request,
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

            Chat? chat = await this._chatRepository.GetByIdAsync(request.ChatId, cancellationToken);
            if (chat is null)
            {
                return Result.Failure(ChatErrors.NotFound);
            }

            if (
                !await this._userChatRepository.IsUserConnectedToChat(
                    user.Id,
                    chat.Id,
                    cancellationToken
                )
            )
            {
                return Result.Failure(ChatErrors.NotConnected);
            }

            Message message = Message.Create(request.Content, chat.Id, user.Id).Value;

            this._messageRepository.Add(message);

            await this._unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
