using System.ComponentModel.DataAnnotations;

namespace CityBreaks.ValidationAttributes;

// This is not a production-ready validator. It doesn't, for example, cater for users not knowing
// whether to include the leading dot in the list of allowed extensions. However, it will suffice
// for demonstration. It is very similar to the previous validator in that it provides a public
// property enabling the user to pass in a list of accepted file extensions.
public class UploadFileExtensionsAttribute : ValidationAttribute
{
    private IEnumerable<string> _allowedExtensions = Enumerable.Empty<string>();
    public string Extensions { get; set; } = string.Empty;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        _allowedExtensions = Extensions
            .Split(new[] { '\u002C' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(e => e.ToLowerInvariant());

        if (value is IFormFile file && _allowedExtensions.Any())
        {
            var extension = Path.GetExtension(file.FileName.ToLowerInvariant());

            if (_allowedExtensions.Contains(extension))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? $"The file extension must be any of the following: {Extensions}");
        }

        return base.IsValid(value, validationContext);
    }
}