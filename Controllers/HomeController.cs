using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Danger_Money.Models;

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
}
