using Microsoft.EntityFrameworkCore;
using WatchList.Data;
using WatchList.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<WatchListContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WatchListContext")));

builder.Services.AddTransient<DbInitializer>();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var initializer = services.GetRequiredService<DbInitializer>();
        initializer.Initialize();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ошибка при заполнении базы данных");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Watch/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Настраиваем маршруты
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Watch}/{action=Index}/{id?}");

app.Run();