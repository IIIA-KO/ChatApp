using ChatApp.Application.Abstraction.Messaging;
using ChatApp.Domain.Abstraction;
using ChatApp.Domain.Chats;

namespace ChatApp.Application.Chats.DeleteChat
{
    internal sealed class DeleteChatCommandHandler : ICommandHandler<DeleteChatCommand>
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteChatCommandHandler(IChatRepository chatRepository, IUnitOfWork unitOfWork)
        {
            this._chatRepository = chatRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            DeleteChatCommand request,
            CancellationToken cancellationToken
        )
        {
            Chat? chat = await this._chatRepository.GetByIdAsync(request.ChatId, cancellationToken);
            if (chat is null)
            {
                return Result.Failure(ChatErrors.NotFound);
            }

            if (request.UserId != chat.CreatorId)
            {
                return Result.Failure(ChatErrors.NotAuthorized);
            }

            this._chatRepository.Remove(chat);

            await this._unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
