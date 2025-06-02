using Microsoft.AspNetCore.Mvc;

namespace DataGupe.Controllers;
[ApiController]
[Route("[controller]")]
public class TodoController(ITodoRepository todoRepository) : ControllerBase
{
    ITodoRepository _toDoRepository = todoRepository;

    public dynamic GetSomeControllerAndHaveSomeFun()
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public virtual async Task<ActionResult<object>> GetTodos()
    {

        try
        {

            var todosDto = await _toDoRepository.GetTodosAsync();
            return Ok(todosDto);
        }
        catch (Exception ex)
        {

            // Log the exception (optional)
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }
}
