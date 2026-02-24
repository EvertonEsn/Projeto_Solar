using FluentValidation;
using Solar.API.ExceptionHandlers;
using Solar.Application.Validation.ProcedimentoValidator;
using Solar.Application.Validation.ProjetoValidators;
using Solar.CrossCutting.Ioc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateProjetoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateProcedimentoValidator>();
builder.Services.AddProblemDetails();
builder.Services.AddInfrastructureAPI(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.ApplyDatabaseMigrations();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(); 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();