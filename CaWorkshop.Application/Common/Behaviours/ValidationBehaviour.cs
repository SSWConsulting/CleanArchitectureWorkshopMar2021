using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR.Pipeline;
using ValidationException = CaWorkshop.Application.Common.Exceptions.ValidationException;

namespace CaWorkshop.Application.Common.Behaviours
{
    public class ValidationBehavior<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            if (!_validators.Any()) return;

            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task
                .WhenAll(_validators.Select(v =>
                    v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
                throw new ValidationException(failures);
        }
    }
}
