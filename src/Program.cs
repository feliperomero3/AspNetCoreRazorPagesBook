using System.Globalization;
using CityBreaks;
using CityBreaks.Data;
using CityBreaks.Models;
using CityBreaks.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

CultureInfo.DefaultThreadCurrentCulture = new("en-US");

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDefaultIdentity<ApplicationUser>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.SignIn.RequireConfirmedAccount = true;
    })
    .AddEntityFrameworkStores<CityBreaksContext>();

builder.Services.AddRazorPages();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddDbContext<CityBreaksContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// This middleware is registered in exactly the same way as the conventions-based example,
// via the UseMiddleware methods or an extension method.
// But an additional step is also required for IMiddleware based components,
// they must also be registered with the application's service container.
builder.Services.AddScoped<IPAddressMiddleware>();

builder.Services.AddTransient<LifetimeDemoService>();
builder.Services.AddSingleton<SingletonService>();
builder.Services.AddScoped<CityService>();
builder.Services.AddTransient<IEmailSender, EmailService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var cityBreaksContext = scope.ServiceProvider.GetRequiredService<CityBreaksContext>();
    cityBreaksContext.Database.EnsureCreated();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// You will probably want to register this middleware after the static files middleware
// so that it doesn't log the IP address for the same visitor for every requested file.
app.UseIPAddress();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
