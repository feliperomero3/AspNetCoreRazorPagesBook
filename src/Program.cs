using CityBreaks;
using CityBreaks.Authorization;
using CityBreaks.Data;
using CityBreaks.Logging;
using CityBreaks.Models;
using CityBreaks.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = LoggingConfiguration.CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services
    .AddDefaultIdentity<ApplicationUser>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.SignIn.RequireConfirmedAccount = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CityBreaksContext>();

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeAreaFolder("Admin", "/Claims", "AdminPolicy");
});

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    options.AddPolicy("AdminPolicy", policyBuilder => policyBuilder.RequireRole("Admin"));
    options.AddPolicy("RoleAdminPolicy", policyBuilder => policyBuilder.RequireRole("RoleAdmin"));

    options.AddPolicy("ViewRolesPolicy", policyBuilder =>
        policyBuilder.AddRequirements(new ViewRolesRequirement(6))
            .RequireClaim("Permission", "View Roles"));

    options.AddPolicy("EditPropertyPolicy", policyBuilder =>
        policyBuilder.AddRequirements(PropertyOperations.Edit));

    options.AddPolicy("DeletePropertyPolicy", policyBuilder =>
        policyBuilder.AddRequirements(PropertyOperations.Delete));
});

builder.Services.AddSingleton<IAuthorizationHandler, ViewRolesSeniorityRequirementHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ViewRolesIsInRoleRequirementHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, PropertyAuthorizationHandler>();

builder.Services.AddDbContext<CityBreaksContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!);
});

builder.Services.AddScoped<CityBreaksContextInitializer>();

builder.Services.AddScoped<IPAddressMiddleware>();

builder.Services.AddTransient<LifetimeDemoService>();
builder.Services.AddSingleton<SingletonService>();
builder.Services.AddScoped<CityService>();
builder.Services.AddScoped<PropertyService>();
builder.Services.AddSingleton<BookingService>();
builder.Services.AddTransient<IEmailSender, EmailService>();
builder.Services.AddTransient<ILoggerProvider, EmailLoggerProvider>();

builder.Services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromDays(365);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<CityBreaksContextInitializer>();
    await initializer.Initialize();
    await initializer.Seed();
}

if (app.Environment.IsProduction())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStatusCodePagesWithReExecute("/Errors/{0}");
app.UseStaticFiles();

app.UseIPAddress();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.MapBookingEndpoints();

app.Run();

Log.CloseAndFlush();
