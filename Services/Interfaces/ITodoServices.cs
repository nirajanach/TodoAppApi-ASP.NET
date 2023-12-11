using TodoApps.ViewModels;

namespace TodoApps.Services.Interfaces
{
    public interface ITodoServices
    {
        public Task<List<TodoViewModel>> GetTodos(int userId);
        public Task<TodoViewModel> GetTodoById(int id, int UserId);

        public Task<int> CreateTodo(CreateTodoViewModel model, int userId);

        public Task<TodoViewModel> UpdateTodo(UpdateTodoViewModel model, int userId);

        public Task<bool> RemoveTodo(int TodoId, int userId);

    }
}
