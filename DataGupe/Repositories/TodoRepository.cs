using DataGupe.Controllers;

public class TodoRepository : ITodoRepository
{
    private readonly Supabase.Client _supabase;
    public TodoRepository(Supabase.Client client) => _supabase = client;

    public async Task<IEnumerable<ToDoList>> GetTodosAsync()
    {
        var todos = await _supabase.From<ToDoLists>().Get();
        if (todos.Models == null || !todos.Models.Any())
        {
            throw new InvalidDataException("No todos found.");
        }
        var todosDto = todos.Models.Select(todo => new ToDoList
        {
            Id = todo.Id,
            CreatedAt = todo.CreatedAt,
            Name =  todo.Name
        }).ToList();
        return todosDto;
    }
}
