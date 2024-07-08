using ChatApp.Application.Abstraction.Messaging;
using ChatApp.Domain.Abstraction;
using ChatApp.Domain.Chats;
using ChatApp.Domain.Users;

namespace ChatApp.Application.Chats.CreateChat
{
    internal sealed class CreateChatCommandHandler : ICommandHandler<CreateChatCommand, ChatId>
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateChatCommandHandler(
            IChatRepository chatRepository,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork
        )
        {
            this._chatRepository = chatRepository;
            this._userRepository = userRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result<ChatId>> Handle(
            CreateChatCommand request,
            CancellationToken cancellationToken
        )
        {
            User? user = await this._userRepository.GetByIdAsync(
                request.CreatorId,
                cancellationToken
            );
            if (user is null)
            {
                return Result.Failure<ChatId>(UserErrors.NotFound);
            }

            Result<Chat> result = Chat.Create(request.ChatName);
            if (result.IsFailure)
            {
                return Result.Failure<ChatId>(result.Error);
            }

            Chat chat = result.Value;

            chat.SetCreator(user);

            this._chatRepository.Add(chat);

            await this._unitOfWork.SaveChangesAsync(cancellationToken);

            return chat.Id;
        }
    }
}
