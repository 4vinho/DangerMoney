namespace Danger_Money;
using Danger_Money.Models.DTOs;

public interface IExpenseRepository
{
    public Task<PagedResponse<IEnumerable<ExpenseDTO>?>> GetWithFilterAsync(ExpenseFilterDTO expenseFilterDTO, PagedRequest request);
    public Task<PagedResponse<IEnumerable<ExpenseDTO>?>> GetThisMonthAsync(PagedRequest request);
    public Task<PagedResponse<IEnumerable<ExpenseDTO>?>> GetByDateAsync(DateTime dateInitial, DateTime dateFinal, PagedRequest request);
    public Task<Response<ExpenseDTO?>> GetByIdAsync(int id);
    public Task<Response<ExpenseDTO?>> PostAsync(ExpenseDTO expenseDTO);
    public Task<Response<ExpenseDTO?>> PutAsync(ExpenseDTO expenseDTO);
    public Task<Response<bool?>> DeleteAsync(int id);
    public Task<Response<IEnumerable<ExpenseCategoryStatsDTO>>> GetExpenseCategoryStatsAsync();
    public Task<Response<IEnumerable<MonthlyExpenseTrendDTO>>> GetMonthlyExpenseTrendAsync(int months = 6);
    public Task<Response<IEnumerable<TopExpenseDTO>>> GetTopExpensesAsync(int count = 5);
}
