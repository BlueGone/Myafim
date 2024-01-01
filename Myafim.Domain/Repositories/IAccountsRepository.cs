using Myafim.Domain.Filters;
using Myafim.Domain.Models;
using Pagination.EntityFrameworkCore.Extensions;

namespace Myafim.Domain.Repositories;

public interface IAccountsRepository
{
    Task<Pagination<Account>> ListAsync(AccountsFilters accountsFilters, int page, int limit, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Account>> ListRawAsync(AccountsFilters accountsFilters, CancellationToken cancellationToken = default);
    Task<long> GetBalanceAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Account>> CreateRangeAsync(IReadOnlyCollection<Account> accounts, CancellationToken cancellationToken = default);
}