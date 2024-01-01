using Microsoft.EntityFrameworkCore;
using Myafim.Domain.Filters;
using Myafim.Domain.Models;
using Myafim.Domain.Repositories;
using Myafim.Infrastructure.FiltersExtensions;
using Pagination.EntityFrameworkCore.Extensions;

namespace Myafim.Infrastructure.Repositories;

public class AccountsRepository(MyafimDbContext context) : IAccountsRepository
{
    public async Task<Pagination<Account>> ListAsync(AccountsFilters accountsFilters, int page, int limit, CancellationToken cancellationToken = default)
    {
        return await context.Accounts
            .ApplyFilters(accountsFilters)
            .AsPaginationAsync(page, limit, cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyCollection<Account>> ListRawAsync(AccountsFilters accountsFilters, CancellationToken cancellationToken = default)
    {
        return await context.Accounts
            .ApplyFilters(accountsFilters)
            .ToListAsync(cancellationToken);
    }

    public async Task<long> GetBalanceAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Accounts
            .Where(account => account.Id == id)
            .Select(account => account.IncomingTransactions.Sum(transaction => transaction.Amount) -
                               account.OutgoingTransactions.Sum(transaction => transaction.Amount))
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<Account>> CreateRangeAsync(IReadOnlyCollection<Account> accounts, CancellationToken cancellationToken = default)
    {
        await context.Accounts.AddRangeAsync(accounts, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return accounts;
    }
}