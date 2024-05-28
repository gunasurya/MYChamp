using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using MYChamp.Controller;
using MYChamp.Controllers;
using MYChamp.DbContexts;
using MYChamp.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorPages();
builder.Services.AddDbContext<MYChampDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("TrooperCruitPostgreSQL")));


builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireUppercase = true;
})
.AddEntityFrameworkStores<MYChampDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
});
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<CardRegistrationController>();
builder.Services.AddScoped<LoginController>();
builder.Services.AddScoped<SessionHandlerController>();
// builder.Services.AddHostedService<ForcefulLogoutBackgroundService>();

builder.Services.AddHttpClient();

// Register the Swagger generator


var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
}


app.UseSession();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
