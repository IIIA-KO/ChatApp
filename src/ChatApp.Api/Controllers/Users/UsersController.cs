using ChatApp.Application.Users.RegisterUser;
using ChatApp.Domain.Users;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers.Users
{
    [Route("api/users")]
    public class UsersController : BaseApiController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterUserRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new RegisterUserCommand(new UserName(request.Username));

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }
    }
}
