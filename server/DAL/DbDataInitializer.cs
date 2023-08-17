using server.Models;  // пространство имен модели User
using Microsoft.AspNetCore.Identity;
using ThreadingTask = System.Threading.Tasks.Task;

namespace server.DAL;

public class DbDataInitializer
{
    public static async ThreadingTask InitializeAsync(
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        ILogger logger)
    {
        if (await roleManager.FindByNameAsync("admin") == null)
        {
            await roleManager.CreateAsync(new IdentityRole("admin"));
        }

        if (await roleManager.FindByNameAsync("user") == null)
        {
            await roleManager.CreateAsync(new IdentityRole("user"));

        }

        if (await userManager.FindByNameAsync("admin") == null)
        {
            const string adminUsername = "admin";
            const string adminPassword = "admin";

            User admin = new User { UserName = adminUsername };
            IdentityResult result = await userManager.CreateAsync(admin, adminPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "admin");
            }
            else
            {
                foreach (var e in result.Errors)
                {
                    logger.LogError($"{e.Code}: {e.Description}");
                }

            }
        }

        if (await userManager.FindByNameAsync("user") == null)
        {
            const string username = "user";
            const string password = "0123456789";

            User user = new User { UserName = username };
            IdentityResult result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "user");
            }
            else
            {
                foreach (var e in result.Errors)
                {
                    logger.LogError($"{e.Code}: {e.Description}");
                }
            }
        }
    }

}
