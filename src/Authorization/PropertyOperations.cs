using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace CityBreaks.Authorization;

/// <summary>
/// Wrapper class for multiple authorization requirements relating to operations on the Property type.
/// </summary>
public class PropertyOperations
{
    public static readonly OperationAuthorizationRequirement Create = new() { Name = nameof(Create) };
    public static readonly OperationAuthorizationRequirement Read = new() { Name = nameof(Read) };
    public static readonly OperationAuthorizationRequirement Edit = new() { Name = nameof(Edit) };
    public static readonly OperationAuthorizationRequirement Delete = new() { Name = nameof(Delete) };
}
