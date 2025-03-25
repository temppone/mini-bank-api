using ApiTest.Repositories;
using ApiTest.Repositories.Interfaces;
using ApiTest.Services;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient();

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IBalanceRepository, BalanceRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<CNPJService>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<TransactionService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

using (var connection = new NpgsqlConnection(connectionString))
{
    connection.Open();
    var command = new NpgsqlCommand("SELECT version()", connection);
    var version = command.ExecuteScalar()?.ToString() ?? "Unknown";

    Console.WriteLine($"PostgreSQL version: {version}");
}

app.Run();
