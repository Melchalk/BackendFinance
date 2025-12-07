namespace BackendFinance.Models.Incomes;

public class GetIncomesResponse
{
    public Income[] Incomes { get; set; } = [];

    public class Income
    {
        public int Id { get; set; }

        public int Amount { get; set; }
    }
}

