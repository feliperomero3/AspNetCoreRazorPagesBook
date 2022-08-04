using System.Globalization;
using CityBreaks;
using CityBreaks.Services;

CultureInfo.DefaultThreadCurrentCulture = new("en-US");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// This middleware is registered in exactly the same way as the conventions-based example,
// via the UseMiddleware methods or an extension method.
// But an additional step is also required for IMiddleware based components
// they must also be registered with the application's service container.
builder.Services.AddScoped<IPAddressMiddleware>();

builder.Services.AddScoped<SimpleCityService>();
builder.Services.AddTransient<LifetimeDemoService>();
builder.Services.AddSingleton<SingletonService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

// You will probably want to register this middleware after the static files middleware
// so that it doesn’t log the IP address for the same visitor for every requested file.
app.UseIPAddress();

app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
