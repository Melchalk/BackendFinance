namespace BackendFinance.Models.Expenses;

public class GetExpensesResponse
{
    public Expense[] Expenses { get; set; } = [];

    public class Expense
    {
        public int Id { get; set; }

        public int Amount { get; set; }

        public DateOnly CreatedAt { get; set; }
    }
}

