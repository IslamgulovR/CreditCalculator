using CreditCalculatorV3.Services;
using CreditCalculatorV3.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssemblyContaining<CreateScheduleDtoValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddTransient<PaymentScheduleService>();

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();
app.UseRouting();

app.Run();