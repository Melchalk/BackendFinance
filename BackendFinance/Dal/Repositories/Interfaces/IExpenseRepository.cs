using BackendFinance.Dal.Entities;

namespace BackendFinance.Dal.Repositories.Interfaces;

public interface IExpenseRepository
{
    Task<int> Add(ExpenseEntityV1 expense, CancellationToken token);

    Task<ExpenseEntityV1[]> GetAll(CancellationToken token);
}
