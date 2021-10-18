using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace StudySharp.API.Helpers
{
    public class FluentValidatorPipelineValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator> _validators;

        public FluentValidatorPipelineValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var validationFailures = _validators
                .Select(validator => validator.Validate(new ValidationContext<TRequest>(request)))
                .SelectMany(validationResult => validationResult.Errors)
                .Where(validationFailure => validationFailure != null)
                .ToList();

            if (validationFailures.Any())
            {
                throw new ValidationException(validationFailures);
            }

            return next?.Invoke();
        }
    }
}
