using BackendFinance.Dal.Entities;
using BackendFinance.Dal.Repositories.Interfaces;
using BackendFinance.Dal.Settings;
using Dapper;
using Microsoft.Extensions.Options;

namespace BackendFinance.Dal.Repositories;

public class ExpenseRepository(IOptions<DalOptions> dalSettings) : PgRepository(dalSettings.Value), IExpenseRepository
{
    public async Task<int> Add(ExpenseEntityV1 expense, CancellationToken token)
    {
        const string sqlQuery =
            """
            insert into expenses (amount, created_at) values (@Amount, @CreatedAt)
            returning id;
            """;

        await using var connection = await GetConnection();
        var ids = await connection.QueryAsync<int>(
            new CommandDefinition(
                sqlQuery,
                new
                {
                    expense.Amount,
                    expense.CreatedAt,
                },
                cancellationToken: token));

        return ids.Single();
    }

    public async Task<ExpenseEntityV1[]> GetAll(CancellationToken token)
    {
        var sqlQuery = """
                        select *
                          from expenses
                        """;

        var cmd = new CommandDefinition(
            sqlQuery,
            cancellationToken: token);

        await using var connection = await GetConnection();

        return [.. await connection.QueryAsync<ExpenseEntityV1>(cmd)];
    }
}
