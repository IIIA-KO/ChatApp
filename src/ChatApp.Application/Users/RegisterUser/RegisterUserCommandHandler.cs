using ChatApp.Application.Abstraction.Messaging;
using ChatApp.Domain.Abstraction;
using ChatApp.Domain.Users;

namespace ChatApp.Application.Users.RegisterUser
{
    internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, UserId>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this._userRepository = userRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result<UserId>> Handle(
            RegisterUserCommand request,
            CancellationToken cancellationToken
        )
        {
            bool userExists = await this._userRepository.UserExists(
                request.UserName,
                cancellationToken
            );

            if (userExists)
            {
                return Result.Failure<UserId>(UserErrors.DuplicateUsername);
            }

            Result<User> result = User.Create(request.UserName);

            if (result.IsFailure)
            {
                return Result.Failure<UserId>(result.Error);
            }

            User user = result.Value;

            this._userRepository.Add(user);

            await this._unitOfWork.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
