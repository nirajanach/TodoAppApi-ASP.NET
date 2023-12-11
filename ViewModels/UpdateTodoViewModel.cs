using System.Globalization;

namespace TodoApps.ViewModels
{
    public class UpdateTodoViewModel
    {
        public int Id { get; set; }
        public string Details { get; set; }
        public bool IsCompleted { get; set; }
    }
}
