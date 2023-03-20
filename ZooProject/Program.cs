using Microsoft.EntityFrameworkCore;
using NLog.Web;
using ZooProject.Data;
using ZooProject.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

#region SQL Connection

var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<ZooContext>(options => options.UseSqlServer(connectionString));
#endregion

builder.Services.AddScoped<IRepository, ZooRepository>();

builder.Logging.ClearProviders();
builder.Host.UseNLog();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ZooContext>();
    dbContext?.Database.EnsureDeleted();
    dbContext?.Database.EnsureCreated();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Display}/{action=DisplayTop2}/{id?}");

app.Run();
