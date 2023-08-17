using Microsoft.AspNetCore.Identity;
using server.Models;
using System.Text.RegularExpressions;
using Task = System.Threading.Tasks.Task;

namespace server.DAL
{
    public class PasswordValidator : IPasswordValidator<User>
    {
        public async Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string password)
        {
            const int requiredLength = 8;
            List<IdentityError> errors = new List<IdentityError>();

            if (user.UserName == "admin")
                return IdentityResult.Success;

            if (string.IsNullOrEmpty(password) || password.Length < requiredLength)
            {
                errors.Add(new IdentityError
                {
                    Description = $"Минимальная длина пароля равна {requiredLength}"
                });
            }
            string pattern = "^[0-9]+$";

            if (!Regex.IsMatch(password, pattern))
            {
                errors.Add(new IdentityError
                {
                    Description = "Пароль должен состоять только из цифр"
                });
            }
            return  errors.Count == 0
                    ? IdentityResult.Success
                    : IdentityResult.Failed(errors.ToArray());

        }
    }
}
