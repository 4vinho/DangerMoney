
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;

namespace Danger_Money;

public class UserServices(
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager
) : IUserServices
{
    public async Task<Response<bool>> Login(LoginDTO loginDTO)
    {
        try
        {
            var result = await signInManager.PasswordSignInAsync(
                loginDTO.Email,
                loginDTO.Password,
                loginDTO.RememberMe,
                lockoutOnFailure: true
            );

            if (result.IsLockedOut)
                return new Response<bool>(400, "maximum login attempts reached", false);

            return new Response<bool>(404, "Incorrect Password or Email", false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(500, $"Internal Error: {ex}", false);
        }

    }

    public async Task<Response<bool>> Logout()
    {
        try
        {
            await signInManager.SignOutAsync();

            return new Response<bool>(200, "Logged out successfully", true);
        }
        catch (Exception ex)
        {
            return new Response<bool>(500, $"Internal Error: {ex}", false);
        }
    }

    public async Task<Response<bool>> Register(RegisterDTO registerDTO)
    {
        try
        {
            if (registerDTO.Password != registerDTO.ConfirmPassword)
                return new Response<bool>(400, "Password are different", false);

            var user = new IdentityUser
            {
                UserName = registerDTO.Name,
                Email = registerDTO.Email,
            };

            var result = await userManager.CreateAsync(user, password: registerDTO.Password);

            if (result.Succeeded)
                return new Response<bool>(200, "Registered successfully", true);

            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return new Response<bool>(400, $"{errors}", false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(500, $"Internal Error: {ex}", false);
        }
    }
}
