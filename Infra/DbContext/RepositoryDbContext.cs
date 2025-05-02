using Microsoft.EntityFrameworkCore;

namespace Danger_Money;

public class RepositoryDbContext : DbContext
{
    public RepositoryDbContext(DbContextOptions<RepositoryDbContext> options)
    : base(options) { }
    public DbSet<Expense> Expenses { get; set; }
}
