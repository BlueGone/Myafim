using Myafim.Domain.Filters;
using Myafim.Domain.Models;
using Pagination.EntityFrameworkCore.Extensions;

namespace Myafim.Domain.Repositories;

public interface ITransactionsRepository
{
    Task<Pagination<Transaction>> ListAsync(TransactionsFilters filters, int page, int limit,
        CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Transaction>> CreateRangeAsync(IReadOnlyCollection<Transaction> transactions, CancellationToken cancellationToken = default);
}