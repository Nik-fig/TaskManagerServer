using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using server.Models;
using System.Reflection.Emit;
using Task = server.Models.Task;
using TaskStatus = server.Models.TaskStatus;

namespace server.DAL
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskStatus> TaskStatuses { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) 
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {   
            builder.Entity<TaskStatus>().HasData(
                new TaskStatus { Id = 1, Name = "Новая" },
                new TaskStatus { Id = 2, Name = "Принята к исполнению" },
                new TaskStatus { Id = 3, Name = "Выполнена" }
                );
            base.OnModelCreating(builder);
        }


    }
}
