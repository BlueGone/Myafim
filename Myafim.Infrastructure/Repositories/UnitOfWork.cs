using Microsoft.EntityFrameworkCore.Storage;
using Myafim.Domain.Repositories;

namespace Myafim.Infrastructure.Repositories;

public class UnitOfWork(MyafimDbContext dbContext) : IUnitOfWork
{
    public async Task WithTransactionAsync(Func<Task> funcAsync, CancellationToken cancellationToken = default)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        await funcAsync();
        await transaction.CommitAsync(cancellationToken);
    }

    public async Task<TResult> WithTransactionAsync<TResult>(Func<Task<TResult>> funcAsync, CancellationToken cancellationToken = default)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        var result = await funcAsync();
        await transaction.CommitAsync(cancellationToken);
        return result;
    }
}