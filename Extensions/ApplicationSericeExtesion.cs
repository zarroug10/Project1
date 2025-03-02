using web.Data.Repository;
using web.Interfaces;

namespace web.Extensions;

public static class ApplicationSericeExtesion
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        return services;
    }
}
