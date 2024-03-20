using BulletinBoard.Server.Consumers;
using BulletinBoard.Server.Contracts;
using BulletinBoard.Server.Persistance;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<MainContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnectionString")));

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<MessageConsumer>()
        .Endpoint(e => e.Name = "advertisements");
    x.AddRequestClient<GetAllAdsContract>(new Uri("exchange:advertisements"));
    x.AddRequestClient<CreateAdContract>(new Uri("exchange:advertisements"));
    x.UsingInMemory((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseWebSockets();

app.UseAuthorization();

app.MapControllers();

app.Run();