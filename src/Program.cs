using CityBreaks;
using CityBreaks.Authorization;
using CityBreaks.Data;
using CityBreaks.Models;
using CityBreaks.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using static CityBreaks.Pages.CityModel;

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
builder.Services.AddSingleton<IAuthorizationHandler, ViewRolesIsInRoleRequirimentHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, PropertyAuthorizationHandler>();

builder.Services.AddDbContext<CityBreaksContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<CityBreaksContextInitializer>();

// This middleware is registered in exactly the same way as the conventions-based example,
// via the UseMiddleware methods or an extension method.
// But an additional step is also required for IMiddleware based components,
// they must also be registered with the application's service container.
builder.Services.AddScoped<IPAddressMiddleware>();

builder.Services.AddTransient<LifetimeDemoService>();
builder.Services.AddSingleton<SingletonService>();
builder.Services.AddScoped<CityService>();
builder.Services.AddScoped<PropertyService>();
builder.Services.AddSingleton<BookingService>();
builder.Services.AddTransient<IEmailSender, EmailService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<CityBreaksContextInitializer>();
    await initializer.Initialize();
    await initializer.Seed();
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

app.MapPost("/properties/booking", async (
    PropertyService propertyService,
    BookingService bookingService,
    BookingInputModel model) =>
{
    if (model.Property is null || model.StartDate is null || model.EndDate is null)
    {
        return Results.Ok(new { TotalCost = "$0.00" });
    }

    var property = await propertyService.GetByIdAsync(model.Property.Id);

    if (property is null)
    {
        return Results.Ok(new { TotalCost = "$0.00" });
    }

    var booking = new Booking
    {
        StartDate = model.StartDate.Value,
        EndDate = model.EndDate.Value,
        NumberOfGuests = model.NumberOfGuests,
        DayRate = property.DayRate
    };

    var totalCost = bookingService.Calculate(booking);

    return Results.Ok(new { TotalCost = totalCost.ToString("C") });
});

app.Run();
