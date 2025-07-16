using Microsoft.EntityFrameworkCore;
using ResourceAndBooking.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register DbContext with connection string from appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Other middleware here
app.UseRouting();
app.UseStaticFiles();
app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();