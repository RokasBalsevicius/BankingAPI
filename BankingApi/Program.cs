using BankingApi.Database;
using BankingApi.Repositories;
using BankingApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<BankingDb>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();