using BackendFinance.Models.Expenses;
using BackendFinance.Models.Incomes;

namespace BackendFinance.Bll;

public interface ITransactionsService
{
    Task<int> GetTotal(CancellationToken token);

    Task<int> GetTotalExpenses(CancellationToken token);

    Task<int> GetTotalIncomes(CancellationToken token);

    Task<GetExpensesResponse> GetExpenses(CancellationToken token);

    Task<GetIncomesResponse> GetIncomes(CancellationToken token);

    Task<int> AddExpense(int amount, CancellationToken token);

    Task<int> AddIncome(int amount, CancellationToken token);
}
