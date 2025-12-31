using TodoApi.Models.DTOs;
 
namespace TodoApi.Services;

public class TodoService
{
    private readonly List<Todo> _todos = new();
    private readonly ILogger<TodoService> _logger;

    public TodoService(ILogger<TodoService> logger)
    {
        _logger = logger;
        _logger.LogInformation("TodoService initialized.");
    }

    private int _nextId = 1;

    public List<Todo> GetAll()
    {
        _logger.LogInformation("Fetching all todos.");
        return _todos;
    }

    public Todo? GetById(int id)
    {
        _logger.LogInformation($"Fetching todo with id {id}.");
        return _todos.FirstOrDefault(t => t.Id == id);
    }

    public Todo Create(Todo todo)
    {
        todo.Id = _nextId++;
        todo.CreatedAt = DateTime.Now;
        _todos.Add(todo);
        _logger.LogInformation($"Todo with id {todo.Id} created.");
        return todo;
    }

    public bool Update(int id, UpdateTodoDTO updatedTodo)
    {
        var todo = GetById(id);
        _logger.LogInformation($"Attempting to update todo with id {id}.");
        _logger.LogInformation($"UpdatedTodo values: Title={updatedTodo.Title}, Description={updatedTodo.Description}, IsCompleted={updatedTodo.IsCompleted}");
        if (todo == null)
        {
            _logger.LogWarning($"Attempted to update non-existing todo with id {id}.");
            return false;
        }

        _logger.LogInformation($"Updating todo with id {id}.");
        if (!string.IsNullOrEmpty(updatedTodo.Title))
        {
            todo.Title = updatedTodo.Title;
        }
        if (!string.IsNullOrEmpty(updatedTodo.Description))
        {
            todo.Description = updatedTodo.Description;
        }
        if (updatedTodo.IsCompleted.HasValue)
        {
            todo.IsCompleted = updatedTodo.IsCompleted.Value;
        }
        return true;
    }

    public bool Delete(int id)
    {
        var todo = GetById(id);
        if (todo == null)
        {
            _logger.LogWarning($"Attempted to delete non-existing todo with id {id}.");
            return false;
        }

        _todos.Remove(todo);
        _logger.LogInformation($"Todo with id {id} deleted.");
        return true;
    }

    // Example method with logging
    public void DoSomething()
    {
        _logger.LogInformation("Doing something in TodoService.");
        // Add your service logic here
    }
}