using MassTransit;
using Microsoft.AspNetCore.Mvc;
using TodoList.Contracts;

namespace TodoList.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TodoController(ILogger<TodoController> logger, IRequestClient<GetAllTodos> getAlltodosRequestClient, IRequestClient<CreateTodoItem> createTodoItemRequestClient, IRequestClient<DeleteTodoItem> deleteTodoItemRequestClient) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllTodos()
    {
        try
        {
            var response = await getAlltodosRequestClient.GetResponse<GetAllTodosResponse>(new GetAllTodos());

            if (response is null)
                return NotFound();

            return Ok(response.Message.Items);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return Problem();
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodo([FromBody] CreateTodoItem request)
    {
        try
        {
            var response = await createTodoItemRequestClient.GetResponse<CreateTodoItemResponse>(request);

            return Created();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return Problem();
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteTodoItem([FromQuery] DeleteTodoItem request)
    {
        try
        {
            var response = await deleteTodoItemRequestClient.GetResponse<DeleteTodoItemResponse>(request);

            if (response is null)
                return Problem();

            return response.Message.NotFound ? NotFound() : Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return Problem();
        }
    }
}
