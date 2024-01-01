using Myafim.Domain.Filters;
using Myafim.Domain.Models;
using Pagination.EntityFrameworkCore.Extensions;

namespace Myafim.Domain.Repositories;

public interface ITransactionsRepository
{
    Task<Pagination<Transaction>> ListAsync(TransactionsFilters filters, int page, int limit,
        CancellationToken cancellationToken = default);
    Task<Transaction?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<Transaction> CreateAsync(Transaction transaction, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<Transaction>> CreateRangeAsync(IReadOnlyCollection<Transaction> transactions, CancellationToken cancellationToken = default);
}