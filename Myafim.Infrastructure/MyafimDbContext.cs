using Microsoft.EntityFrameworkCore;

namespace Myafim.Infrastructure;

public class MyafimDbContext : DbContext
{
    public MyafimDbContext() : this(new DbContextOptionsBuilder<MyafimDbContext>().UseSqlite().Options) { }
    public MyafimDbContext(DbContextOptions<MyafimDbContext> options) : base(options) { }
}
