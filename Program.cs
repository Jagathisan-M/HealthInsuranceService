using HealthInsuranceService.Controllers;
using HealthInsuranceService.DBFramework;
using HealthInsuranceService.HealthInsuranceDBContext;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string ConnectionString = builder.Configuration["ConnectionStrings:DBConnection"];
builder.Services.AddDbContext<HealthInsuranceContext>(options => options.UseSqlServer(ConnectionString));

builder.Services.AddScoped<UserDetailDB>();
builder.Services.AddScoped<AcquirerPlanDB>();
builder.Services.AddScoped<InsurancePlanDB>();
builder.Services.AddScoped<PaymentCycleDB>();
//builder.Services.AddScoped<PaymentScheduleDB>();

var app = builder.Build();

app.UseExceptionHandler(handler =>
{
    handler.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerPathFeature>();

        context.Response.ContentType = "application/json";

        if (exception?.Error is SqlException)
        {
            context.Response.StatusCode = 547;
        }
        else if (exception?.Error is UnauthorizedAccessException)
        {
            context.Response.StatusCode = 401;
        }
        else
        {
            context.Response.StatusCode = 500;
        }

        await context.Response.WriteAsync("Internal Server Error");
    });
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
