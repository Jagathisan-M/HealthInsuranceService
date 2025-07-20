using HealthInsuranceService.Controllers;
using HealthInsuranceService.DBFramework;
using HealthInsuranceService.HealthInsuranceDBContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Configuration;
using System.Text;

using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string ConnectionString = builder.Configuration["ConnectionStrings:DBConnection"];
builder.Services.AddDbContext<HealthInsuranceContext>(options => 
        options.UseSqlServer(ConnectionString)
);

builder.Services.AddScoped<UserDetailDB>();
builder.Services.AddScoped<AcquirerPlanDB>();
builder.Services.AddScoped<InsurancePlanDB>();
builder.Services.AddScoped<PaymentCycleDB>();
builder.Services.AddScoped<PaymentScheduleDB>();

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = builder.Configuration["Jwt:Issuer"],
//            ValidAudience = builder.Configuration["Jwt:Audience"],
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
//        };
//    });

builder.Services.AddAuthorization();

//Start : To log in Files-----------------------

//Nuget Pacage : 
//Serilog.AspNetCore
//Serilog.Sinks.File

//Log.Logger = new LoggerConfiguration()
//    .WriteTo.Console()
//    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
//    .CreateLogger();
//builder.Host.UseSerilog();

//End : To log in Files-----------------------

//Start : log in Console----------------------

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

//End : log in Console------------------------

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

app.MapControllers();

app.Run();
