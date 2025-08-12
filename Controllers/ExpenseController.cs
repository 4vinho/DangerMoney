using Danger_Money;
using Microsoft.AspNetCore.Mvc;

namespace Danger_Money;

public class ExpenseController(
    IExpenseRepository expenseRepository
) : Controller
{
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ExpenseDTO expenseDTO)
    {
        if (!ModelState.IsValid)
            return View(expenseDTO);

        var response = await expenseRepository.PostAsync(expenseDTO);

        if (!response.IsSuccess)
        {
            ModelState.AddModelError("", response.Message);
            return View(expenseDTO);
        }

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Index(int pageCount = 1, int pageSize = 10)
    {
        var pagedRequest = new PagedRequest { PageCount = pageCount, PageSize = pageSize };
        var response = await expenseRepository.GetThisMonthAsync(pagedRequest);

        if (!response.IsSuccess)
        {
            ViewBag.ErrorMessage = response.Message;
        }

        return View(response);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var response = await expenseRepository.GetByIdAsync(id);
        if (!response.IsSuccess)
        {
            ViewBag.ErrorMessage = response.Message;
            return View("Error"); // Or a more specific error view
        }
        return View(response.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ExpenseDTO expenseDTO)
    {
        if (!ModelState.IsValid)
            return View(expenseDTO);

        var response = await expenseRepository.PutAsync(expenseDTO);
        if (!response.IsSuccess)
        {
            ModelState.AddModelError("", response.Message);
            return View(expenseDTO);
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await expenseRepository.GetByIdAsync(id);
        if (!response.IsSuccess)
        {
            ViewBag.ErrorMessage = response.Message;
            return View("Error"); // Or a more specific error view
        }
        return View(response.Data);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var response = await expenseRepository.DeleteAsync(id);
        if (!response.IsSuccess)
        {
            ViewBag.ErrorMessage = response.Message;
            return View("Error"); // Or a more specific error view
        }
        return RedirectToAction(nameof(Index));
    }

}

