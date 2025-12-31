using Microsoft.AspNetCore.Mvc;
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
    public ActionResult<List<Todo>> GetAll()
    {
        return Ok(_todoService.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Todo> GetById(int id)
    {
        var todo = _todoService.GetById(id);
        if (todo == null)
            return NotFound();

        return Ok(todo);
    }

    [HttpPost]
    public ActionResult<Todo> Create(Todo todo)
    {
        var created = _todoService.Create(todo);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateTodoDTO todo)
    {
        if (_todoService.Update(id, todo))
            return NoContent();

        return NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (_todoService.Delete(id))
            return Ok(new { message = "Silme başarılı" });

        return NotFound();
    }
}