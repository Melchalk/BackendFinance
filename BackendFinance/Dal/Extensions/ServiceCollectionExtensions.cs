using BackendFinance.Dal.Infrastructure;
using BackendFinance.Dal.Settings;

namespace BackendFinance.Dal.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDalInfrastructure(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.Configure<DalOptions>(config.GetSection(nameof(DalOptions)));

        Postgres.MapCompositeTypes();

        Postgres.AddMigrations(services);

        return services;
    }
}
