using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Danger_Money.Models;
using Danger_Money.Models.DTOs;
using Danger_Money.Models.ViewModels;

namespace Danger_Money.Controllers;

public class HomeController(
    IExpenseRepository expenseRepository
) : Controller
{
    public async Task<IActionResult> Index()
    {
        var pagedRequest = new PagedRequest { PageCount = 1, PageSize = 10 };
        var response = await expenseRepository.GetThisMonthAsync(pagedRequest);

        if (!response.IsSuccess)
        {
            ViewBag.ErrorMessage = response.Message;
            return View(new List<ExpenseDTO>());
        }

        return View(response.Data);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public async Task<IActionResult> Statistics()
    {
        var categoryStatsResponse = await expenseRepository.GetExpenseCategoryStatsAsync();
        var monthlyTrendResponse = await expenseRepository.GetMonthlyExpenseTrendAsync();
        var topExpensesResponse = await expenseRepository.GetTopExpensesAsync();

        var viewModel = new StatisticsViewModel
        {
            CategoryStats = categoryStatsResponse.IsSuccess ? categoryStatsResponse.Data : new List<ExpenseCategoryStatsDTO>(),
            MonthlyTrend = monthlyTrendResponse.IsSuccess ? monthlyTrendResponse.Data : new List<MonthlyExpenseTrendDTO>(),
            TopExpenses = topExpensesResponse.IsSuccess ? topExpensesResponse.Data : new List<TopExpenseDTO>()
        };

        if (!categoryStatsResponse.IsSuccess || !monthlyTrendResponse.IsSuccess || !topExpensesResponse.IsSuccess)
        {
            var errorMessage = "Failed to retrieve statistics data:";
            if (!categoryStatsResponse.IsSuccess) errorMessage += "\n- Category Stats: " + categoryStatsResponse.Message;
            if (!monthlyTrendResponse.IsSuccess) errorMessage += "\n- Monthly Trend: " + monthlyTrendResponse.Message;
            if (!topExpensesResponse.IsSuccess) errorMessage += "\n- Top Expenses: " + topExpensesResponse.Message;
            ViewBag.ErrorMessage = errorMessage;
        }

        return View(viewModel);
    }
}
