using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StaffManagement.Application.SharedKernel;
using StaffManagement.Domain.EmployeePositions;
using StaffManagement.Domain.Employees;
using StaffManagement.Domain.Positions;
using StaffManagement.Infrastructure.EmployeePositions;
using StaffManagement.Infrastructure.Employees;
using StaffManagement.Infrastructure.Positions;
using StaffManagement.Infrastructure.SharedKernel;

namespace StaffManagement.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddScoped<IConnectionFactory>(_ => new ConnectionFactory(configuration["ConnectionStrings:Postgres"]))
            .AddRepositories();
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IEmployeeRepository, EmployeeRepository>()
            .AddScoped<IPositionRepository, PositionRepository>()
            .AddScoped<IEmployeePositionRepository, EmployeePositionRepository>();
    }
}