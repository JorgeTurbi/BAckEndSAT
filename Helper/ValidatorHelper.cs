namespace Helper;

using System.ComponentModel.DataAnnotations;
using DTOs;

public static class ValidatorHelper
{
    public static GenericResponseDto<T> Validate<T>(T obj)
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(obj!);
        bool isValid = Validator.TryValidateObject(obj!, context, results, true);

        if (!isValid)
        {
            var allErrors = string.Join(" | ", results.Select(r => r.ErrorMessage));
            return GenericResponseDto<T>.Fail(allErrors);
        }

        return GenericResponseDto<T>.Ok(obj);
    }
}
