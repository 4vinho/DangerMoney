namespace Danger_Money;

public class ExpenseFilterDTO
{
    public DateTime? dateInitial { get; set; }
    public DateTime? dateFinal { get; set; }
    public decimal? amountInitial { get; set; }
    public decimal? amountFinal { get; set; }
    public ExpenseTypeEnum? type { get; set; }
}
