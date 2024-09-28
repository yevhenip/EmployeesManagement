using EmployeesManagement.Core.UseCases.Common.Behaviours;
using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeesManagement.Core.UseCases;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration config)
    {
        return services
            .AddMapper()
            .AddMediator();
    }

    private static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddSingleton<TypeAdapterConfig>(_ =>
        {
            var typeAdapterConfig = new TypeAdapterConfig();

            typeAdapterConfig.Scan(AssemblyMarker.Assembly);

            return typeAdapterConfig;
        });
        services.AddSingleton<IMapper, ServiceMapper>();

        return services;
    }

    private static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(AssemblyMarker.Assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        services.AddValidatorsFromAssembly(AssemblyMarker.Assembly);

        return services;
    }
}