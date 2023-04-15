using ToDoAPI.Data;
using ToDoAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDbContext<AppDbContext>(options => options
        .UseSqlServer(builder.Configuration.GetConnectionString("SampleDbConnection")));

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("api/todo", async (AppDbContext context)
    => Results.Ok(await context.ToDos.ToListAsync()));

app.MapPost("api/todo", async (AppDbContext context, ToDo toDo) =>
{
    await context.ToDos.AddAsync(toDo);

    await context.SaveChangesAsync();

    return Results.Created($"api/todo/{toDo.Id}", toDo);
});

app.Run();