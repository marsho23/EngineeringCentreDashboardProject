//using EngineeringCentreDashboard.Business;
//using EngineeringCentreDashboard.Data;
//using EngineeringCentreDashboard.Interfaces;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using System.Configuration;

//var builder = WebApplication.CreateBuilder(args);
//// Add services to the container.

//builder.Services.AddDbContext<ToDoDbContext>(options =>
//    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection")));

////string connectionString = builder.Configuration.GetConnectionString("PostgreSqlConnection");
////builder.Services.AddDbContext<ToDoDbContext>(options=>
////    options.UseNpgsql(connectionString));

//builder.Services.AddScoped<IToDoHelper, ToDoHelper>();
//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapDefaultControllerRoute();

//app.Run();

using EngineeringCentreDashboard.Business;
using EngineeringCentreDashboard.Data;
using EngineeringCentreDashboard.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddDbContext<ToDoDbContext>(options =>
//    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection")));

builder.Services.AddDbContext< ToDoDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection")));


builder.Services.AddScoped<IToDoHelper, ToDoHelper>();
builder.Services.AddControllers();

// Add Swagger/OpenAPI
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
