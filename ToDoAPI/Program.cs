using ToDoAPI.Data;
using ToDoAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDbContext<AppDbContext>(options => options
        .UseSqlServer(builder.Configuration.GetConnectionString("SampleDbConnection")));

var app = builder.Build();

app.MapGet("api/todo", async (AppDbContext context)
    => Results.Ok(await context.ToDos.ToListAsync()));

app.MapPost("api/todo", async (AppDbContext context, ToDo toDo) =>
{
    await context.ToDos.AddAsync(toDo);

    await context.SaveChangesAsync();

    return Results.Created($"api/todo/{toDo.Id}", toDo);
});

app.MapPut("api/todo/{id}", async (AppDbContext context, int id, ToDo toDo) =>
{
    var toDoModel = await context.ToDos
        .FirstOrDefaultAsync(t => t.Id == id);

    if (toDoModel == null)
    {
        return Results.NotFound();
    }

    toDoModel.ToDoName = toDo.ToDoName;

    await context.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("api/todo/{id}", async (AppDbContext context, int id, ToDo toDo) =>
{
    var toDoModel = await context.ToDos
        .FirstOrDefaultAsync(t => t.Id == id);

    if (toDoModel == null)
    {
        return Results.NotFound();
    }

    context.ToDos.Remove(toDoModel);

    await context.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();