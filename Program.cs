using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MYChamp.DbContexts;
using MYChamp.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDbContext<MYChampDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("TrooperCruitPostgreSQL")));

builder.Services.AddIdentity<AppUser, IdentityRole>(
    options =>
    {
        options.Password.RequiredUniqueChars = 0;
        options.Password.RequireUppercase = true;
    })
    .AddEntityFrameworkStores<MYChampDbContext>()
    .AddDefaultTokenProviders();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
