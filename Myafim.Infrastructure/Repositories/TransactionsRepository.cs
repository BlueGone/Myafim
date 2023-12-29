using Myafim.Domain.Models;
using Myafim.Domain.Repositories;
using Pagination.EntityFrameworkCore.Extensions;

namespace Myafim.Infrastructure.Repositories;

public class TransactionsRepository(MyafimDbContext context) : ITransactionsRepository
{
    public async Task<Pagination<Transaction>> ListAsync(int page, int limit)
    {
        return await context.Transactions.AsPaginationAsync(page, limit);
    }

    public async Task<IReadOnlyCollection<Transaction>> CreateRangeAsync(IReadOnlyCollection<Transaction> transactions)
    {
        await context.Transactions.AddRangeAsync(transactions);
        await context.SaveChangesAsync();
        return transactions;
    }
}