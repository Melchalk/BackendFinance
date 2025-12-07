using BackendFinance.Bll;
using BackendFinance.Models.Expenses;
using BackendFinance.Models.Incomes;
using Microsoft.AspNetCore.Mvc;

namespace BackendFinance.Controllers;

[Route("api/transactions")]
[ApiController]
public class TransactionsController([FromServices] ITransactionsService transactionsService) : ControllerBase
{
    [HttpGet("total")]
    public async Task<int> GetTotal(CancellationToken token)
    {
        return await transactionsService.GetTotal(token);
    }

    [HttpGet("total/expenses")]
    public async Task<int> GetTotalExpenses(CancellationToken token)
    {
        return await transactionsService.GetTotalExpenses(token);
    }

    [HttpGet("total/incomes")]
    public async Task<int> GetTotalIncomes(CancellationToken token)
    {
        return await transactionsService.GetTotalIncomes(token);
    }

    [HttpGet("incomes")]
    public async Task<GetIncomesResponse> GetIncomes(CancellationToken token)
    {
        return await transactionsService.GetIncomes(token);
    }

    [HttpGet("expenses")]
    public async Task<GetExpensesResponse> GetExpenses(CancellationToken token)
    {
        return await transactionsService.GetExpenses(token);
    }

    [HttpPost("expense")]
    public async Task<int> AddExpense([FromQuery] int amount, CancellationToken token)
    {
        return await transactionsService.AddExpense(amount, token);
    }

    [HttpPost("income")]
    public async Task<int> AddIncome([FromQuery] int amount, CancellationToken token)
    {
        return await transactionsService.AddIncome(amount, token);
    }
}
