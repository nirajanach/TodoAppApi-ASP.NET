using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApps.Services.Interfaces;
using TodoApps.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApps.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class TodoAPIController : BaseAuthController
    {
        private readonly ITodoServices _todoService;


        public TodoAPIController(ITodoServices todoService)
        {
            _todoService = todoService;
        }

        // GET: api/<TodoAPIController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(typeof(IEnumerable<TodoViewModel>))]
        public async Task<IActionResult> Get([FromQuery] bool? isCompleted)
        {

            var todos = await _todoService.GetTodos(GetUserId());
            if (isCompleted == null)
            {
                return Ok(todos);
            }

            var filtered = todos
                .Where(todos => todos.IsCompleted == isCompleted)
                .ToList();
            return Ok(filtered);

        }


        // GET api/<TodoAPIController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(typeof(TodoViewModel))]
        public async Task<IActionResult> GetTodoById(int todoId)
        {
            if (todoId == null)
            {
                RedirectToAction("Get");
            }
            var todo = await _todoService.GetTodoById(todoId, GetUserId());
            if (todo == null)
            {
                return NotFound("No Todo Found");
            }
            return Ok(todo);
         
        }

        // POST api/<TodoAPIController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateTodo([FromBody] CreateTodoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = await _todoService.CreateTodo(model, GetUserId());

            return Ok(id);
        }


        // PUT api/<TodoAPIController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(typeof(TodoViewModel))]
        public async Task<IActionResult> UpdateTodoById([FromBody]UpdateTodoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todo = await _todoService.UpdateTodo(model, GetUserId());

            return Ok(todo);
        }

        // DELETE api/<TodoAPIController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> DeleteTodoById(int id)
        {
            var todoid = await _todoService.RemoveTodo(id, GetUserId());
            return Ok(todoid);
           
        }
    }
}
