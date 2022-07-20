using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CityBreaks.ValidationAttributes;

/// <summary>
/// Specifies that the first letter of two properties must match.
/// </summary>
public class CompareFirstLetterAttribute : ValidationAttribute
{
    public string? OtherProperty { get; set; }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (OtherProperty == null)
        {
            return new ValidationResult("You must specify another property to compare to.");
        }

        var otherPropertyInfo = validationContext.ObjectType.GetRuntimeProperty(OtherProperty);

        if (otherPropertyInfo == null)
        {
            return new ValidationResult("You must specify another property to compare to.");
        }

        var otherValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);

        if (otherValue == null)
        {
            return new ValidationResult("You must specify another property to compare to.");
        }

        if (!string.IsNullOrWhiteSpace(value.ToString()) &&
            !string.IsNullOrWhiteSpace(otherValue.ToString()) &&
            value.ToString()!.ToLower().First() != otherValue.ToString()!.ToLower().First())
        {
            return new ValidationResult(ErrorMessage
                ?? $"The first letters of {validationContext.DisplayName} and {otherPropertyInfo.Name} must match.");
        }

        return ValidationResult.Success;
    }
}