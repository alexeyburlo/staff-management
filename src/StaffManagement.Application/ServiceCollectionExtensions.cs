using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace StaffManagement.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        return services
            .AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));
    }
}