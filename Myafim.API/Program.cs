using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Myafim.API.Endpoints;
using Myafim.Domain;
using Myafim.FireflyIii.Client;
using Myafim.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFireflyIiiClient();
builder.Services.AddHandlers();
builder.Services.AddRepositories();

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

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MyafimDbContext>();
    await dbContext.Database.EnsureCreatedAsync();
}

app.RegisterTransactionsEndpoints();
app.RegisterAccountsEndpoints();
app.RegisterCategoriesEndpoints();
app.RegisterImportFireflyIiiEndpoints();

app.Run();