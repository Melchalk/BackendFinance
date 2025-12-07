using BackendFinance.Dal.Entities;
using BackendFinance.Dal.Settings;
using FluentMigrator.Runner;
using Microsoft.Extensions.Options;
using Npgsql;
using Npgsql.NameTranslation;

namespace BackendFinance.Dal.Infrastructure;

public static class Postgres
{
    private static readonly INpgsqlNameTranslator Translator = new NpgsqlSnakeCaseNameTranslator();

    public static void MapCompositeTypes()
    {
        var mapper = NpgsqlConnection.GlobalTypeMapper;
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        mapper.MapComposite<ExpenseEntityV1>("expense_v1", Translator);
        mapper.MapComposite<IncomeEntityV1>("income_v1", Translator);
    }

    public static void AddMigrations(IServiceCollection services)
    {
        services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb.AddPostgres()
                .WithGlobalConnectionString(s =>
                {
                    var cfg = s.GetRequiredService<IOptions<DalOptions>>();
                    return cfg.Value.PostgresConnectionString;
                })
                .ScanIn(typeof(Postgres).Assembly).For.Migrations()
            )
            .AddLogging(lb => lb.AddFluentMigratorConsole());
    }
}
