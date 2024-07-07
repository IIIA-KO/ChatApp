using ChatApp.Domain.Abstraction;
using MediatR;

namespace ChatApp.Application.Abstraction.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>> { }
}
