using server.Models;  // пространство имен модели User
using Microsoft.AspNetCore.Identity;
using ThreadingTask = System.Threading.Tasks.Task;

namespace server.DAL;

public class RoleInitializer
{
    public static async ThreadingTask InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
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

            User admin = new User { UserName = adminUsername};
            IdentityResult result = await userManager.CreateAsync(admin, adminPassword);
           
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "admin");
            } else
            {
                foreach (var e in result.Errors)
                {
                    Console.WriteLine($"{e.Code}: {e.Description}");
                }
                
            }
        }
    }

}
