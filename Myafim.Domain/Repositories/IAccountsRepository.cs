using Myafim.Domain.Models;
using Pagination.EntityFrameworkCore.Extensions;

namespace Myafim.Domain.Repositories;

public interface IAccountsRepository
{
    Task<Pagination<Account>> ListAsync(int page, int limit);
    Task<long> GetBalanceAsync(int id);
    Task<IReadOnlyCollection<Account>> CreateRangeAsync(IReadOnlyCollection<Account> accounts);
}