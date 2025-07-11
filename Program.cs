
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RoboIAZoho.classes;
using RoboIAZoho.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ZohoCrmApiClient>();
builder.Services.AddScoped<ZohoDeskApiClient>();
builder.Services.AddScoped<ZohoProjectsApiClient>();
builder.Services.AddScoped<IZohoProjectService, ZohoProjectService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
