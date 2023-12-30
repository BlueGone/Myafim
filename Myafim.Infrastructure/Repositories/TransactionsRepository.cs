using Myafim.Domain.Models;
using Myafim.Domain.Repositories;
using Pagination.EntityFrameworkCore.Extensions;

namespace Myafim.Infrastructure.Repositories;

public class TransactionsRepository(MyafimDbContext context) : ITransactionsRepository
{
    public async Task<Pagination<Transaction>> ListAsync(int page, int limit, CancellationToken cancellationToken = default)
    {
        return await context.Transactions.AsPaginationAsync(page, limit, cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyCollection<Transaction>> CreateRangeAsync(IReadOnlyCollection<Transaction> transactions, CancellationToken cancellationToken = default)
    {
        await context.Transactions.AddRangeAsync(transactions, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return transactions;
    }
}