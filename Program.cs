using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MYChamp.DbContexts;
using MYChamp.Handlers;
using MYChamp.MinimalApi;
using MYChamp.Models.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<MYChampDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("TrooperCruitPostgreSQL")));

builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<MYChampDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AddPageRoute("/Pages/Auth/Login", "/");
});
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<SessionHandler>();
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

// Define minimal API endpoints
app.MapEndpoints();

// Map Razor Pages
app.MapRazorPages();

// Configure default endpoint for handling root access
app.MapGet("/", async context =>
{
    if (!context.User.Identity.IsAuthenticated)
    {
        context.Response.Redirect("/Auth/Login");
    }
    else
    {
        context.Response.Redirect("/Index");
    }
});

// Run the application
app.Run();
