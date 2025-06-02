using DataGupe.Controllers;

public interface ITodoRepository
{
    Task<IEnumerable<ToDoList>> GetTodosAsync();
}