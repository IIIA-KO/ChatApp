using ChatApp.Domain.Users;

namespace ChatApp.Application.UnitTests.Users
{
    internal static class UserData
    {
        public static User Create() => User.Create(UserName).Value;

        public static readonly UserName UserName = new("UserName");
    }
}
