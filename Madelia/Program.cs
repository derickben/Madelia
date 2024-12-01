using Microsoft.EntityFrameworkCore;
using Madelia.Models;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Getting connection string from app settings
var cs = builder.Configuration.GetConnectionString("MADELIA_CS");

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    {
        // Configure circular reference handling
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });

// DI for TripDBContext and SQL DB
builder.Services.AddDbContext<MadeliaDBContext>(
    options => options.UseSqlServer(cs)
);

// Set a session
builder.Services.AddSession(options => options.IdleTimeout = TimeSpan.FromSeconds(60 * 5));

// Configure lowercase URLs
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "details",
    pattern: "{controller=Home}/{action=Details}/{id?}");

app.MapControllerRoute(
    name: "withCategory",
    pattern: "{controller=Home}/{action=Index}/{activeCategory?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
