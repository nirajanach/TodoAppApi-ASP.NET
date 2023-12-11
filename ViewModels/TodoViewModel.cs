using System.ComponentModel.DataAnnotations.Schema;
using TodoApps.Models;

namespace TodoApps.ViewModels
{
    public class TodoViewModel
    {
        public int Id { get; set; }
        public string Details { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsCompleted { get; set; }              
        public int UserId { get; set; } 
    }
}
