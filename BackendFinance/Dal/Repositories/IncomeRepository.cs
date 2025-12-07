using BackendFinance.Dal.Entities;
using BackendFinance.Dal.Repositories.Interfaces;
using BackendFinance.Dal.Settings;
using Dapper;
using Microsoft.Extensions.Options;

namespace BackendFinance.Dal.Repositories;

public class IncomeRepository(IOptions<DalOptions> dalSettings) : PgRepository(dalSettings.Value), IIncomeRepository
{
    public async Task<int> Add(IncomeEntityV1 income, CancellationToken token)
    {
        const string sqlQuery =
            """
            insert into incomes (amount, created_at) values (@Amount, @CreatedAt)
            returning id;
            """;

        await using var connection = await GetConnection();
        var ids = await connection.QueryAsync<int>(
            new CommandDefinition(
                sqlQuery,
                new
                {
                    income.Amount,
                    income.CreatedAt,
                },
                cancellationToken: token));

        return ids.Single();
    }

    public async Task<IncomeEntityV1[]> GetAll(CancellationToken token)
    {
        var sqlQuery = """
                        select *
                          from incomes
                        """;

        var cmd = new CommandDefinition(
            sqlQuery,
            cancellationToken: token);

        await using var connection = await GetConnection();

        return [.. await connection.QueryAsync<IncomeEntityV1>(cmd)];
    }
}
