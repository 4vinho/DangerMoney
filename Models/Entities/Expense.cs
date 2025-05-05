namespace Danger_Money;

public class Expense
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal Amount { get; set; }

    public DateTime Date { get; set; }

    public ExpenseTypeEnum Type { get; set; }

    public int UserId { get; set; }
}
