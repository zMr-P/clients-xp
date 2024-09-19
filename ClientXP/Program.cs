using ClientXP.Domain.Entities;
using ClientXP.Domain.Validations;
using ClientXP.Infraestructure.Config;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigContext();
builder.Services.ConfigServices();
builder.Services.AddScoped<IValidator<Client>, ClientValidation>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
app.EnsureDatabaseCreated();

app.Run();
