namespace Danger_Money.Models.ViewModels
{
    using Danger_Money.Models.DTOs;
    public class StatisticsViewModel
    {
        public IEnumerable<ExpenseCategoryStatsDTO> CategoryStats { get; set; }
        public IEnumerable<MonthlyExpenseTrendDTO> MonthlyTrend { get; set; }
        public IEnumerable<TopExpenseDTO> TopExpenses { get; set; }
    }
}