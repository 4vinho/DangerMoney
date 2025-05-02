using System.Threading.Tasks;
using Danger_Money;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserServiceController(
        IUserServices userServices
    ) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var response = await userServices.Login(loginDTO);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var response = await userServices.Register(registerDTO);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            var response = await userServices.Logout();

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
