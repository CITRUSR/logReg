using FluentValidation;
using MediatR;

namespace LogReg.Application.common.behaviours;

public class ValidationBehaviour<TRequest, TReponse>(IEnumerable<IValidator> validators)
    : IPipelineBehavior<TRequest, TReponse>
    where TRequest : IRequest<TReponse>
{
    private readonly IEnumerable<IValidator> _validators = validators;

    public async Task<TReponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TReponse> next,
        CancellationToken cancellationToken
    )
    {
        var context = new ValidationContext<TRequest>(request);

        var validationsFailures = await Task.WhenAll(
            _validators.Select(validator => validator.ValidateAsync(context))
        );

        var errors = validationsFailures
            .Where(x => !x.IsValid)
            .SelectMany(validationFailure => validationFailure.Errors)
            .ToList();

        if (errors.Count != 0)
        {
            throw new ValidationException(errors);
        }

        return await next();
    }
}
