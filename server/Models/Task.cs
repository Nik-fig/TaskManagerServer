using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models
{
    public class Task
    {
        public int Id { get; set; }
        public User User{ get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime DateLastChange { get; set; }
        public TaskStatus Status { get; set; }

    }
}
