using UserApi.Models;
using UserApi.Repositories;
using UserApi.Repositories.Impl;
using UserApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddSingleton<IRepository<User, string>, DictionaryUserRepository>();
builder.Services.AddSingleton<UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();
app.Run();
