using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApps.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string? Details { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }

        [ForeignKey("Id")]
        [InverseProperty("Todos")]
        public virtual ICollection<User> UserIds { get; set; } = new List<User>();
    }
}
