using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Danger_Money.Models.DTOs;

namespace Danger_Money;

public class ExpenseRepository(
    RepositoryDbContext context,
    IMapper mapper
) : IExpenseRepository
{
    public async Task<Response<ExpenseDTO?>> GetByIdAsync(int id)
    {
        try
        {
            var expense = await context.Expenses.FirstOrDefaultAsync(x => x.Id == id);
            if (expense == null)
                return new Response<ExpenseDTO?>(404, "Expense not found", null);

            var expenseDTO = mapper.Map<ExpenseDTO>(expense);
            return new Response<ExpenseDTO?>(200, "Expense retrieved successfully", expenseDTO);
        }
        catch (Exception ex)
        {
            return new Response<ExpenseDTO?>(500, $"Internal Error: {ex.Message}", null);
        }
    }

    public async Task<Response<bool?>> DeleteAsync(int id)
    {
        try
        {
            var expense = await context.Expenses.FirstOrDefaultAsync(x => x.Id == id);
            if (expense == null)
                return new Response<bool?>(404, "Not found", false);

            context.Expenses.Remove(expense);
            await context.SaveChangesAsync();

            return new Response<bool?>(200, "Delete successfully", true);
        }
        catch (Exception ex)
        {
            return new Response<bool?>(500, $"Internal Error: {ex.Message}", false);
        }
    }

    public async Task<PagedResponse<IEnumerable<ExpenseDTO>?>> GetByDateAsync(DateTime dateInitial, DateTime dateFinal, PagedRequest request)
    {
        var query = context.Expenses
            .Where(x => x.Date >= dateInitial && x.Date <= dateFinal);

        var totalItems = await query.CountAsync();

        var expenses = await query
            .OrderByDescending(x => x.Id)
            .Skip((request.PageCount - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        var result = mapper.Map<IEnumerable<ExpenseDTO>>(expenses);

        return new PagedResponse<IEnumerable<ExpenseDTO>?>(
            200,
            "Retrieved by date successfully",
            result,
            request.PageSize,
            totalItems
        );
    }

    public async Task<PagedResponse<IEnumerable<ExpenseDTO>?>> GetThisMonthAsync(PagedRequest request)
    {
        var now = DateTime.Now;
        var firstDay = new DateTime(now.Year, now.Month, 1);
        var lastDay = firstDay.AddMonths(1).AddDays(-1);

        return await GetByDateAsync(firstDay, lastDay, request);
    }

    public async Task<PagedResponse<IEnumerable<ExpenseDTO>?>> GetWithFilterAsync(ExpenseFilterDTO filter, PagedRequest request)
    {
        var query = context.Expenses.AsQueryable();

        if (filter.dateInitial.HasValue)
            query = query.Where(x => x.Date >= filter.dateInitial);

        if (filter.dateFinal.HasValue)
            query = query.Where(x => x.Date <= filter.dateFinal);

        if (filter.amountInitial.HasValue)
            query = query.Where(x => x.Amount >= filter.amountInitial);

        if (filter.amountFinal.HasValue)
            query = query.Where(x => x.Amount <= filter.amountFinal);

        if (filter.type.HasValue)
            query = query.Where(x => x.Type == filter.type);

        var totalItems = await query.CountAsync();

        var expenses = await query
            .Skip((request.PageCount - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        var result = mapper.Map<IEnumerable<ExpenseDTO>>(expenses);

        return new PagedResponse<IEnumerable<ExpenseDTO>?>(
            200,
            "Filtered expenses retrieved successfully",
            result,
            request.PageSize,
            totalItems
        );
    }

    public async Task<Response<ExpenseDTO?>> PostAsync(ExpenseDTO expenseDTO)
    {
        try
        {
            var expense = mapper.Map<Expense>(expenseDTO);

            await context.Expenses.AddAsync(expense);
            await context.SaveChangesAsync();

            var expenseResponse = mapper.Map<ExpenseDTO>(expense);

            return new Response<ExpenseDTO?>(200, "Created successfully", expenseResponse);
        }
        catch (Exception ex)
        {
            return new Response<ExpenseDTO?>(500, $"Internal Error: {ex.Message}", null);
        }
    }

    public async Task<Response<ExpenseDTO?>> PutAsync(ExpenseDTO expenseDTO)
    {
        try
        {
            var existing = await context.Expenses.FirstOrDefaultAsync(x => x.Id == expenseDTO.Id);
            if (existing == null)
                return new Response<ExpenseDTO?>(404, "Expense not found", null);

            mapper.Map(expenseDTO, existing);
            await context.SaveChangesAsync();

            return new Response<ExpenseDTO?>(200, "Updated successfully", expenseDTO);
        }
        catch (Exception ex)
        {
            return new Response<ExpenseDTO?>(500, $"Internal Error: {ex.Message}", null);
        }
    }

    public async Task<Response<IEnumerable<ExpenseCategoryStatsDTO>>> GetExpenseCategoryStatsAsync()
    {
        try
        {
            var stats = await context.Expenses
                .GroupBy(e => e.Type)
                .Select(g => new ExpenseCategoryStatsDTO
                {
                    CategoryName = g.Key.ToString(), // Convert enum to string
                    TotalAmount = g.Sum(e => e.Amount)
                })
                .ToListAsync();

            return new Response<IEnumerable<ExpenseCategoryStatsDTO>>(200, "Expense category stats retrieved successfully", stats);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<ExpenseCategoryStatsDTO>>(500, $"Internal Error: {ex.Message}", null);
        }
    }

    public async Task<Response<IEnumerable<MonthlyExpenseTrendDTO>>> GetMonthlyExpenseTrendAsync(int months = 6)
    {
        try
        {
            var endDate = DateTime.Now;
            var startDate = endDate.AddMonths(-months).Date;

            var trendData = (await context.Expenses
                .Where(e => e.Date >= startDate && e.Date <= endDate)
                .GroupBy(e => new { e.Date.Year, e.Date.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalAmount = g.Sum(e => e.Amount)
                })
                .ToListAsync()) // Bring data to client
                .Select(x => new MonthlyExpenseTrendDTO
                {
                    MonthYear = $"{x.Month}/{x.Year}",
                    TotalAmount = x.TotalAmount
                })
                .OrderBy(x => x.MonthYear) // Order by month/year for chronological display
                .ToList();

            return new Response<IEnumerable<MonthlyExpenseTrendDTO>>(200, "Monthly expense trend retrieved successfully", trendData);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<MonthlyExpenseTrendDTO>>(500, $"Internal Error: {ex.Message}", null);
        }
    }

    public async Task<Response<IEnumerable<TopExpenseDTO>>> GetTopExpensesAsync(int count = 5)
    {
        try
        {
            var topExpenses = (await context.Expenses
                .ToListAsync()) // Bring all expenses to client
                .OrderByDescending(e => e.Amount)
                .Take(count)
                .Select(e => new TopExpenseDTO
                {
                    Name = e.Name,
                    Amount = e.Amount
                })
                .ToList();

            return new Response<IEnumerable<TopExpenseDTO>>(200, "Top expenses retrieved successfully", topExpenses);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<TopExpenseDTO>>(500, $"Internal Error: {ex.Message}", null);
        }
    }
}
