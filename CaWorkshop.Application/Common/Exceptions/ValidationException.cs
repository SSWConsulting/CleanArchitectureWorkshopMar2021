using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CaWorkshop.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException()
            : base("One or more validation errors occurred.") { }

        public ValidationException(List<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; } = new Dictionary<string, string[]>();
    }
}
