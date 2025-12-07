using BackendFinance.Bll;
using BackendFinance.Dal.Extensions;
using BackendFinance.Dal.Repositories;
using BackendFinance.Dal.Repositories.Interfaces;
using BackendFinance.Swagger;
using FluentMigrator.Runner;

namespace BackendFinance;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddMvc();

        services
            .AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                );
            });

        services.AddControllers();

        AddDal(services);
        AddBllServices(services);

        services.AddEndpointsApiExplorer();

        services.AddSwagger();
        services.AddGrpcSwagger();

        services.AddHttpContextAccessor();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseCors("CorsPolicy");

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    public void AddBllServices(IServiceCollection services)
    {
        services.AddScoped<ITransactionsService, TransactionsService>();
    }

    public void AddDal(IServiceCollection services)
    {
        services.AddScoped<IExpenseRepository, ExpenseRepository>();
        services.AddScoped<IIncomeRepository, IncomeRepository>();

        services.AddDalInfrastructure(Configuration);

        MigrateUp(services);
    }

    public void MigrateUp(IServiceCollection services)
    {
        using var scope = services.BuildServiceProvider().CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }
}
