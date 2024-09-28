using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ValidationException = EmployeesManagement.Core.Exceptions.ValidationException;

namespace EmployeesManagement.Core.UseCases.Common.Behaviours;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IServiceProvider _serviceProvider;

    public ValidationBehavior(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var errors = _serviceProvider
            .GetServices<IValidator<TRequest>>()
            .Select(x => x.Validate(request))
            .SelectMany(x => x.Errors)
            .GroupBy(x => x.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.ErrorMessage).ToArray()
            );

        if (!errors.Any())
        {
            return next();
        }

        throw new ValidationException(errors);
    }
}