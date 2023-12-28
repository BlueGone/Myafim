using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Myafim.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var sqliteConnection = new SqliteConnection("Data Source=:memory:");
await sqliteConnection.OpenAsync();
builder.Services.AddDbContext<MyafimDbContext>(optionsBuilder =>
{
    optionsBuilder.UseSqlite(sqliteConnection);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
