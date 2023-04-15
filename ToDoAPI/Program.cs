using ToDoAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDbContext<AppDbContext>(options => options
        .UseSqlServer(builder.Configuration.GetConnectionString("SampleDbConnection")));

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();