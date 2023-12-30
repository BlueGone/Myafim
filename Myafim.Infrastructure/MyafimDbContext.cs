using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Myafim.Domain.Models;

namespace Myafim.Infrastructure;

public class MyafimDbContext : DbContext
{
    public DbSet<Transaction> Transactions { get; set; } = null!;
    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    
    public MyafimDbContext() : this(new DbContextOptionsBuilder<MyafimDbContext>().UseSqlite().Options) { }
    public MyafimDbContext(DbContextOptions<MyafimDbContext> options) : base(options) { }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<Rune>()
            .HaveConversion<RuneConverter>();

        configurationBuilder
            .Properties<DateTimeOffset>()
            .HaveConversion<DateTimeOffsetToBinaryConverter>();

        base.ConfigureConventions(configurationBuilder);
    }
}
