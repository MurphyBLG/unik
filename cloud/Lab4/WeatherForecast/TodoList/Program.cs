using MassTransit;
using Microsoft.EntityFrameworkCore;
using TodoList.Consumers;
using TodoList.Contracts;
using TodoList.Persistance;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddDbContext<MainContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnectionString")));


    builder.Services.AddMassTransit(x =>
    {
        x.AddConsumer<TodoConsumer>()
            .Endpoint(e => e.Name = $"{nameof(TodoConsumer)}");
        x.AddRequestClient<GetAllTodos>(new Uri($"exchange:{nameof(TodoConsumer)}"));
        x.AddRequestClient<CreateTodoItem>(new Uri($"exchange:{nameof(TodoConsumer)}"));
        x.AddRequestClient<DeleteTodoItem>(new Uri($"exchange:{nameof(TodoConsumer)}"));

        x.UsingInMemory((context, cfg) =>
        {
            cfg.ConfigureEndpoints(context);
        });
    });

}


var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}

