using CityBreaks;

System.Globalization.CultureInfo.DefaultThreadCurrentCulture = new System.Globalization.CultureInfo("en-US");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

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
