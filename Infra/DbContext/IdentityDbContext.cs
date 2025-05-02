using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Danger_Money;

public class IdentityDbContext(DbContextOptions options) : IdentityDbContext<IdentityUser>(options)
{

}
