using Application.UseCases;
using FluentValidation.Results;

namespace Application.Extensions
{
    public static class ValidationResultExtensions
    {
        public static Result ToResult(this ValidationResult validationResult)
        {
            if (validationResult.IsValid)
                return Result.Ok();

            var errors = validationResult.Errors
                .Select(e => e.ErrorMessage);

            return Result.Fail(errors);
        }
    }
}
