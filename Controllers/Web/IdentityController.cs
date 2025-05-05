using Danger_Money;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
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

                Console.WriteLine(response.ToString());

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

                Console.WriteLine(response.ToString());

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
    }
}
