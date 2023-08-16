using Microsoft.AspNetCore.Identity;
using server.Models;
using Task = System.Threading.Tasks.Task;

namespace server.DAL
{
    public class AdminPasswordValidator : IPasswordValidator<User>
    {
        public async Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string password)
        {
            return user.UserName == "admin"
                    ? IdentityResult.Success
                    : IdentityResult.Failed();

        }
    }
}
