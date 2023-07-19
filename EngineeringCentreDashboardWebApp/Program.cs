using EngineeringCentreDashboardWebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<IWeatherService, WeatherService>();

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

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
