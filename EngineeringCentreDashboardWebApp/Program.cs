using EngineeringCentreDashboard.Business;
using EngineeringCentreDashboard.Data;
using EngineeringCentreDashboard.Interfaces;
using EngineeringCentreDashboardWebApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EngineeringDashboardDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection")));

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
//})
//   .AddCookie()
//   .AddCookie("Identity.External")
//   //.AddOpenIdConnect(options =>
//   //{
//   //    options.ClientId = "464389598970-ef6kaora26hv8kge7lt4ggrnsti514bu.apps.googleusercontent.com";
//   //    options.ClientSecret = "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";
//   //    options.Authority = "https://accounts.google.com/";
//   //    options.CallbackPath = "/signin-google"; // The callback path where Google should redirect after authentication.
//   //});
//   .AddGoogle(options =>
//   {
//       options.ClientId = "464389598970-ef6kaora26hv8kge7lt4ggrnsti514bu.apps.googleusercontent.com";
//       options.ClientSecret = "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";
//   });

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
    })
        .AddCookie(options =>
        {
            options.LogoutPath = "/Account/Logout"; // Specify the logout path for cookie authentication
        })
        .AddGoogle(options =>
        {
            options.ClientId = "464389598970-ef6kaora26hv8kge7lt4ggrnsti514bu.apps.googleusercontent.com";
            options.ClientSecret = "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";
            options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme; // Set the SignInScheme to the Cookie scheme
        });


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<IWeatherService, WeatherService>();
builder.Services.AddScoped<IUserLoginHelper, UserLoginHelper>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin());
});

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("MyPolicy",
//        builder =>
//        {
//            builder.WithOrigins("https://localhost:7181", "https://localhost:7187", "https://localhost:5432")
//                   .AllowAnyHeader()
//                   .AllowAnyMethod();
//        });
//});





//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//})
//    .AddCookie(options =>
//    {
//        options.LoginPath = "/account/google-login";
//    })
//    .AddGoogle(options =>
//    {
//        options.ClientId = "464389598970-ef6kaora26hv8kge7lt4ggrnsti514bu.apps.googleusercontent.com";
//        options.ClientSecret = "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";
//        options.SignInScheme = IdentityConstants.ExternalScheme;
//    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Use the CORS policy.
app.UseCors("AllowAllOrigins");
app.UseAuthentication();

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
