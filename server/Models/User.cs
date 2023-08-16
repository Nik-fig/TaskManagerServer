using Microsoft.AspNetCore.Identity;
using Task = server.Models.Task;
namespace server.Models
{
    public class User : IdentityUser
    {
        public List<Task>? Tasks { get; set; }
    }
}
