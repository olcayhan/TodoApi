using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Repositories;

public interface ITodoRepository
{
    Task<List<Todo>> ToListAsync();
    Task<Todo?> FindAsync(int id);
    Task AddAsync(Todo todo);
    Task Update(Todo todo);
    Task Remove(Todo todo);
    Task SaveChangesAsync();
}

public class TodoRepository : ITodoRepository
{
    private readonly TodoDbContext _context;
    public TodoRepository(TodoDbContext context)
    {
        _context = context;
    }
    public async Task<List<Todo>> ToListAsync()
    {
        return await _context.Todos.ToListAsync();
    }
    public async Task<Todo?> FindAsync(int id)
    {
        return await _context.Todos.FindAsync(id);
    }
    public async Task AddAsync(Todo todo)
    {
        await _context.Todos.AddAsync(todo);
    }
    public async Task Update(Todo todo)
    {
        _context.Todos.Update(todo);
    }
    public async Task Remove(Todo todo)
    {
        _context.Todos.Remove(todo);
    }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

}