using System.ComponentModel.DataAnnotations;

namespace TodoApps.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
