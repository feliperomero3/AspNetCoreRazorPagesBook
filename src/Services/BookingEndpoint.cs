using CityBreaks.Models;
using static CityBreaks.Pages.CityModel;

namespace CityBreaks.Services;

internal static class BookingEndpoint
{
    public static RouteHandlerBuilder MapBookingEndpoints(this IEndpointRouteBuilder builder)
    {
        return builder.MapPost("/properties/booking", Booking());

        static Func<PropertyService, BookingService, BookingInputModel, Task<IResult>> Booking()
        {
            return async (PropertyService propertyService, BookingService bookingService, BookingInputModel model) =>
            {
                if (model.Property is null || model.StartDate is null || model.EndDate is null)
                {
                    return Results.Ok(new { TotalCost = "$0.00" });
                }

                Property? property = await propertyService.GetByIdAsync(model.Property.Id);

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
            };
        }
    }
}
