using TodoApi.Models;
using TodoApi.Models.DTOs;
using TodoApi.Repositories;

namespace TodoApi.Services;

public interface ITodoService
{
    Task<List<Todo>> GetAllAsync();
    Task<Todo?> GetByIdAsync(int id);
    Task<Todo?> CreateAsync(TodoCreateDTO todo);
    Task<bool> UpdateAsync(int id, UpdateTodoDTO updatedTodo);
    Task<bool> DeleteAsync(int id);
}

public class TodoService : ITodoService
{
    private readonly ILogger<TodoService> _logger;
    private readonly ITodoRepository _repository;

    public TodoService(ITodoRepository repository, ILogger<TodoService> logger)
    {
        _repository = repository;
        _logger = logger;
        _logger.LogInformation("TodoService initialized.");
    }

    public async Task<List<Todo>> GetAllAsync()
    {
        try
        {
            _logger.LogInformation("Fetching all todos.");
            var entities = await _repository.ToListAsync();
            return entities;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching all todos.");
            return new List<Todo>();
        }
    }

    public async Task<Todo?> GetByIdAsync(int id)
    {
        try
        {
            _logger.LogInformation($"Fetching todo with id {id}.");
            var entity = await _repository.FindAsync(id);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while fetching todo with id {id}.");
            return null;
        }
    }

    public async Task<Todo?> CreateAsync(TodoCreateDTO todo)
    {
        try
        {
            var entity = new Todo
            {
                Title = todo.Title,
                Description = todo.Description,
                IsCompleted = false
            };
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
            _logger.LogInformation($"Todo with id {entity.Id} created.");
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating a new todo.");
            return null;
        }
    }

    public async Task<bool> UpdateAsync(int id, UpdateTodoDTO updatedTodo)
    {
        try
        {

            var todo = await GetByIdAsync(id);
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
            await _repository.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while updating todo with id {id}.");
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var todo = await GetByIdAsync(id);
            if (todo == null)
            {
                _logger.LogWarning($"Attempted to delete non-existing todo with id {id}.");
                return false;
            }

            await _repository.Remove(todo);
            await _repository.SaveChangesAsync();
            _logger.LogInformation($"Todo with id {id} deleted.");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while deleting todo with id {id}.");
            return false;
        }
    }
}