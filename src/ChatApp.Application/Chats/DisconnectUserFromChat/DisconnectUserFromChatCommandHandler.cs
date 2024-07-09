using ChatApp.Application.Abstraction.Messaging;
using ChatApp.Domain.Abstraction;
using ChatApp.Domain.Chats;
using ChatApp.Domain.UserChats;
using ChatApp.Domain.Users;

namespace ChatApp.Application.Chats.DisconnectUserFromChat
{
    internal sealed class DisconnectUserFromChatCommandHandler
        : ICommandHandler<DisconnectUserFromChatCommand>
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserChatRepository _userChatRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DisconnectUserFromChatCommandHandler(
            IChatRepository chatRepository,
            IUserRepository userRepository,
            IUserChatRepository userChatRepository,
            IUnitOfWork unitOfWork
        )
        {
            this._chatRepository = chatRepository;
            this._userRepository = userRepository;
            this._userChatRepository = userChatRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            DisconnectUserFromChatCommand request,
            CancellationToken cancellationToken
        )
        {
            User? user = await this._userRepository.GetByIdAsync(request.UserId, cancellationToken);
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

            var userChat = new UserChat { UserId = user.Id, ChatId = chat.Id, };

            this._userChatRepository.RemoveUserFromChat(userChat);

            await this._unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
