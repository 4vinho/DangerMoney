using Microsoft.AspNetCore.Identity;

namespace Danger_Money;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
}
