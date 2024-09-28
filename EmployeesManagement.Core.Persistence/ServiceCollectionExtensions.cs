using EmployeesManagement.Core.Adapters;
using EmployeesManagement.Core.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeesManagement.Core.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceLayer(this IServiceCollection services, IConfiguration config)
    {
        return services
            .AddDatabaseAdapter(config);
    }

    private static IServiceCollection AddDatabaseAdapter(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(new DbConnectionFactory(configuration.GetConnectionString("DbConnection")));
        services.AddSingleton<IEmployeesRepository, EmployeesRepository>();
        services.AddSingleton<IDepartmentsRepository, DepartmentsRepository>();

        return services;
    }
}