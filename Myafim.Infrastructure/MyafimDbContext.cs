using Microsoft.EntityFrameworkCore;
using Myafim.Domain;

namespace Myafim.Infrastructure;

public class MyafimDbContext : DbContext
{
    public DbSet<Transaction> Transactions { get; set; } = null!;
    public DbSet<Account> Accounts { get; set; } = null!;
    
    public MyafimDbContext() : this(new DbContextOptionsBuilder<MyafimDbContext>().UseSqlite().Options) { }
    public MyafimDbContext(DbContextOptions<MyafimDbContext> options) : base(options) { }
}
