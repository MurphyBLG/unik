using AutoMapper;
using MassTransit;
using TodoList.Contracts;
using TodoList.Core;
using TodoList.Persistance;

namespace TodoList.Consumers;

public class TodoConsumer(MainContext repository, ILogger<TodoConsumer> logger, IMapper mapper) : IConsumer<CreateTodoItem>, IConsumer<DeleteTodoItem>, IConsumer<GetAllTodos>
{
    public async Task Consume(ConsumeContext<CreateTodoItem> context)
    {
        try
        {
            repository.TodoItems.Add(mapper.Map<TodoItem>(context.Message));

            await repository.SaveChangesAsync();
            await context.RespondAsync(new CreateTodoItemResponse());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            throw;
        }
    }


    public async Task Consume(ConsumeContext<GetAllTodos> context)
    {
        try
        {
            await context.RespondAsync(new GetAllTodosResponse
            {
                Items = repository.TodoItems.Select(item => mapper.Map<ItemModelBase>(item)).ToList()
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task Consume(ConsumeContext<DeleteTodoItem> context)
    {
        try
        {
            var todo = await repository.TodoItems.FindAsync(context.Message.Id);

            if (todo is null)
            {
                await context.RespondAsync(new DeleteTodoItemResponse { NotFound = true });
                return;
            }

            repository.TodoItems.Remove(todo);
            await repository.SaveChangesAsync();

            await context.RespondAsync(new DeleteTodoItemResponse { NotFound = false });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            throw;
        }
    }
}
