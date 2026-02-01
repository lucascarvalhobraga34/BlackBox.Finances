using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public IReadOnlyList<string> Errors { get; }

        protected Result(bool isSuccess, IReadOnlyList<string> errors)
        {
            IsSuccess = isSuccess;
            Errors = errors;
        }

        public static Result Ok()
            => new(true, Array.Empty<string>());

        public static Result Fail(IEnumerable<string> errors)
            => new(false, errors.ToList());
    }

    public class Result<T> : Result
    {
        public T? Value { get; }

        protected Result(bool isSuccess, T? value, IReadOnlyList<string> errors)
            : base(isSuccess, errors)
        {
            Value = value;
        }

        public static Result<T> Ok(T value)
            => new(true, value, Array.Empty<string>());

        public static new Result<T> Fail(IEnumerable<string> errors)
            => new(false, default, errors.ToList());
    }
}
