using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Models.DTOs;
using TodoApi.Services;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly TodoService _todoService;

    public TodoController(TodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Todo>>> GetAll()
    {
        var todos = await _todoService.GetAllAsync();
        return Ok(todos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Todo>> GetById(int id)
    {
        var todo = await _todoService.GetByIdAsync(id);
        if (todo == null)
            return NotFound();

        return Ok(todo);
    }

    [HttpPost]
    public async Task<ActionResult<Todo>> Create(TodoCreateDTO todo)
    {
        var created = await _todoService.CreateAsync(todo);
        if (created == null)
            return BadRequest();
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateTodoDTO todo)
    {
        var updatedTodo = await _todoService.UpdateAsync(id, todo);
        if (updatedTodo)
            return NoContent();

        return NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deletedTodo = await _todoService.DeleteAsync(id);
        if (deletedTodo)
            return NoContent();
        return NotFound();
    }
}