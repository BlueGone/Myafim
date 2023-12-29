using Myafim.Domain.Models;
using Pagination.EntityFrameworkCore.Extensions;

namespace Myafim.Domain.Repositories;

public interface ITransactionsRepository
{
    Task<Pagination<Transaction>> ListAsync(int page, int limit);
    Task<IReadOnlyCollection<Transaction>> CreateRangeAsync(IReadOnlyCollection<Transaction> transactions);
}