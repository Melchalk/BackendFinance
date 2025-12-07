using BackendFinance.Dal.Entities;

namespace BackendFinance.Dal.Repositories.Interfaces;

public interface IIncomeRepository
{
    Task<int> Add(IncomeEntityV1 income, CancellationToken token);

    Task<IncomeEntityV1[]> GetAll(CancellationToken token);
}
