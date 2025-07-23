using Microsoft.EntityFrameworkCore;
using AdminBlog.Models;
using Microsoft.AspNetCore.StaticAssets;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;

var builder = WebApplication.CreateBuilder(args);
StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BlogContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlogDb")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Statik dosyalar için GEREKLÝ
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
