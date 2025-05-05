namespace Danger_Money;

public interface IExpenseRepository
{
    public Task<PagedResponse<IEnumerable<ExpenseDTO>?>> GetWithFilterAsync(ExpenseFilterDTO expenseFilterDTO, PagedRequest request);
    public Task<PagedResponse<IEnumerable<ExpenseDTO>?>> GetThisMonthAsync(PagedRequest request);
    public Task<PagedResponse<IEnumerable<ExpenseDTO>?>> GetByDateAsync(DateTime dateInitial, DateTime dateFinal, PagedRequest request);
    public Task<Response<ExpenseDTO?>> PostAsync(ExpenseDTO expenseDTO);
    public Task<Response<ExpenseDTO?>> PutAsync(ExpenseDTO expenseDTO);
    public Task<Response<bool?>> DeleteAsync(int id);
}
