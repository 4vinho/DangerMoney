using Danger_Money;
using Microsoft.AspNetCore.Mvc;

namespace Danger_Money
{
    public class IdentityController(
        IUserServices userServices

    ) : Controller
    {

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                var response = await userServices.Login(loginDTO);



                if (response.IsSuccess)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", response.Message ?? "Usuário ou senha inválidos.");
                }
            }
            return View(loginDTO);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (ModelState.IsValid)
            {
                var response = await userServices.Register(registerDTO);



                if (response.IsSuccess)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", response.Message ?? "Usuário ou senha inválidos.");
                }
            }
            return View(registerDTO);
        }

        public ActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await userServices.Logout(); // Assuming IUserServices has a Logout method
            return RedirectToAction("Index", "Home");
        }
    }
}
