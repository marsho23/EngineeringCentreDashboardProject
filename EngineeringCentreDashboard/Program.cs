

using EngineeringCentreDashboard.Business;
using EngineeringCentreDashboard.Data;
using EngineeringCentreDashboard.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static EngineeringCentreDashboard.Business.GoogleCalendarHelper;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<EngineeringDashboardDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection")));



builder.Services.AddScoped<IToDoHelper, ToDoHelper>();
builder.Services.AddScoped<IUserLoginHelper, UserLoginHelper>();
builder.Services.AddScoped<IGoogleCalendarHelper, GoogleCalendarHelper>();
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
    .AddCookie(options =>
    {
        options.LoginPath = "/account/google-login";
    })
    .AddGoogle(options =>
    {
        options.ClientId = "464389598970-ef6kaora26hv8kge7lt4ggrnsti514bu.apps.googleusercontent.com";
        options.ClientSecret = "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";
        options.Scope.Add("profile");
    });

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
app.UseCors("AllowAllOrigins");
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
