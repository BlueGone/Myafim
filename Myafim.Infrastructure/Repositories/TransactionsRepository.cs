using Myafim.Domain.Filters;
using Myafim.Domain.Models;
using Myafim.Domain.Repositories;
using Myafim.Infrastructure.FiltersExtensions;
using Pagination.EntityFrameworkCore.Extensions;

namespace Myafim.Infrastructure.Repositories;

public class TransactionsRepository(MyafimDbContext context) : ITransactionsRepository
{
    public async Task<Pagination<Transaction>> ListAsync(TransactionsFilters filters, int page, int limit, CancellationToken cancellationToken = default)
    {
        return await context.Transactions
            .ApplyFilters(filters)
            .AsPaginationAsync(page, limit, cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyCollection<Transaction>> CreateRangeAsync(IReadOnlyCollection<Transaction> transactions, CancellationToken cancellationToken = default)
    {
        await context.Transactions.AddRangeAsync(transactions, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return transactions;
    }
}