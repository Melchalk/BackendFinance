using BackendFinance.Dal.Entities;
using BackendFinance.Dal.Repositories.Interfaces;
using BackendFinance.Models.Expenses;
using BackendFinance.Models.Incomes;

namespace BackendFinance.Bll;

public class TransactionsService(
    IExpenseRepository expenseRepository,
    IIncomeRepository incomeRepository) : ITransactionsService
{
    public async Task<int> GetTotal(CancellationToken token)
    {
        var totalExpenses = await GetTotalExpenses(token);

        var totalIncomes = await GetTotalIncomes(token);

        return totalIncomes - totalExpenses;
    }

    public async Task<int> GetTotalExpenses(CancellationToken token)
    {
        var expenses = await expenseRepository.GetAll(token);

        return expenses.Select(e => e.Amount).Sum();
    }

    public async Task<int> GetTotalIncomes(CancellationToken token)
    {
        var incomes = await incomeRepository.GetAll(token);

        return incomes.Select(e => e.Amount).Sum();
    }

    public async Task<GetExpensesResponse> GetExpenses(CancellationToken token)
    {
        var expenses = await expenseRepository.GetAll(token);

        return new GetExpensesResponse
        {
            Expenses = expenses.Select(e => new GetExpensesResponse.Expense
            {
                Id = e.Id,
                Amount = e.Amount,
            }).ToArray()
        };
    }

    public async Task<GetIncomesResponse> GetIncomes(CancellationToken token)
    {
        var incomes = await incomeRepository.GetAll(token);

        return new GetIncomesResponse
        {
            Incomes = incomes.Select(e => new GetIncomesResponse.Income
            {
                Id = e.Id,
                Amount = e.Amount,
            }).ToArray()
        };
    }

    public async Task<int> AddExpense(int amount, CancellationToken token)
    {
        var expense = new ExpenseEntityV1 { Amount = amount };

        return await expenseRepository.Add(expense, token);
    }

    public async Task<int> AddIncome(int amount, CancellationToken token)
    {
        var incomes = new IncomeEntityV1 { Amount = amount }; 

        return await incomeRepository.Add(incomes, token);
    }
}
