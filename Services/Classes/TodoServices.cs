using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using TodoApps.Models;
using TodoApps.Services.Interfaces;
using TodoApps.ViewModels;

namespace TodoApps.Services.Classes
{
    public class TodoServices : ITodoServices
    {
        private readonly TodoDbContext _context;

        private readonly IMapper _mapper;

        public TodoServices(TodoDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<List<TodoViewModel>> GetTodos(int userId)
        {
            var todos = await _context.Todos
                .Where(todo => todo.UserId == userId)
                .ToListAsync();

            return _mapper.Map<List<TodoViewModel>>(todos);
        }
         
        public async Task<TodoViewModel> GetTodoById(int todoId, int userId)
        {
            var todo = await _context.Todos
                .FirstOrDefaultAsync(todo => todo.Id == todoId && todo.UserId == userId);

            return todo == null ? null : _mapper.Map<TodoViewModel>(todo);
        }
        public async Task<int> CreateTodo(CreateTodoViewModel model, int userId)
        {
            var todo = new Todo()
            {
                UserId = userId,
                Details = model.Details,
                CreatedAt = DateTime.Now,
                IsCompleted = false

            };
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            return todo.Id;

                
        }

        public async Task<TodoViewModel> UpdateTodo(UpdateTodoViewModel model, int userId)
        {
            var todo = await _context.Todos.FirstOrDefaultAsync(t => t.Id == userId);

            if(todo is null)
            {
                throw new ApplicationException();
            }

            todo.Details = model.Details;
            todo.IsCompleted = model.IsCompleted;

            _context.Todos.Update(todo);
            await _context.SaveChangesAsync();

            return _mapper.Map<TodoViewModel>(todo);

          
        }


        public async Task<bool> RemoveTodo(int TodoId, int userId)
        {
            var found = await GetTodos(userId);
            if(found is null)
            {
                return false;
            }

            var todo = new Todo { Id = userId };

            try
            {
            _context.Todos.Remove(todo);

            await _context.SaveChangesAsync();

                return true;

            } catch (Exception e)
            {
                return false;
            }
        }

    }
}
