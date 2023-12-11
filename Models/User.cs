using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApps.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Password { get; set; }

        [ForeignKey("Id")]
        [InverseProperty("UserIds")]
        public virtual ICollection<Todo> Todos { get; set;} = new List<Todo>();

    }
}
