using DataGupe.Controllers;
using DataGupe.Tests.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Stashbox.Mocking.Moq;
using Supabase;

namespace DataGupeTests;

// Fake controller for testing response logic only
public class FakeTodoController : TodoController
{
    private readonly IEnumerable<ToDoList> _fakeTodos;
    private readonly bool _throwException;

    public FakeTodoController(IEnumerable<ToDoList> fakeTodos, bool throwException = false)
        : base(null!)
    {
        _fakeTodos = fakeTodos;
        _throwException = throwException;
    }

    public override async Task<ActionResult<object>> GetTodos()
    {
        if (_throwException)
        {
            return new ObjectResult("Internal server error: Test error") { StatusCode = StatusCodes.Status500InternalServerError };
        }
        return Ok(_fakeTodos);
    }
}

[TestClass]
public class TodoControllerTests
{
    private StashMoq _stash;

    IEnumerable<ToDoList> _todo = new List<ToDoList>
        {
            new() { Id = 1, CreatedAt = DateTime.UtcNow, Name = "Test" },
            new() { Id = 2, CreatedAt = DateTime.UtcNow, Name = "Test2" }
        };
    private Client _supaBaseClient;

    [TestInitialize]
    public void Setup()
    {
        _stash = CompositionRoot.SetupStashMoq(container =>
        {
            var mockRepo = new Mock<ITodoRepository>();
            var localTodo = _todo; // Capture the instance of 'todo' in a local variable to avoid referencing 'this'.  
            mockRepo.Setup(r => r.GetTodosAsync()).Returns(Task.FromResult(localTodo));
            container.RegisterInstance(mockRepo.Object);
        });

        _supaBaseClient = new Client("https://your-supabase-url.supabase.co", "your-anon-key"); // Replace with your actual Supabase URL and anon key
    }

    // Stash mock data example
    [TestMethod]
    public async Task GetTodos_ReturnsOkResult_WithTodos()
    {
        // Arrange  
        //var controller = new FakeTodoController(_todo);
        var controller = _stash.Get<TodoController>();

        // Act  
        var result = await controller.GetTodos();

        // Assert  
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);

        var todos = okResult.Value as IEnumerable<ToDoList>;
        Assert.IsNotNull(todos);
        Assert.AreEqual(2, todos.Count());
        Assert.AreEqual("Test", todos.First().Name);
    }

    // Test driven development example
    [TestMethod]
    public async Task GetToDoTask_OkResult()
    {
        var controller = _stash.Get<TodoController>();
        // This will fail because we havn't implemented it yet
        dynamic result = controller.GetSomeControllerAndHaveSomeFun();
        Assert.IsNotNull(result);
        Assert.AreEqual(DateTime.Now, result.CreatedDate);
    }

    // Integration test example
    [TestMethod]
    public async Task GetTodosIntegrated_ReturnsOkResult_WithTodos()
    {
        // Arrange  
        var repo = new TodoRepository(_supaBaseClient);
        var controller = new TodoController(repo);

        // Act  
        var result = await controller.GetTodos();

        // Assert  
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);

        var todos = okResult.Value as IEnumerable<ToDoList>;
        Assert.IsNotNull(todos);
        Assert.AreEqual(1, todos.Count());
        Assert.IsTrue(todos.First().Name.Contains("Alan"));
    }
}
