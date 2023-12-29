using Microsoft.EntityFrameworkCore;
using Myafim.Domain.Models;
using Myafim.Domain.Repositories;
using Pagination.EntityFrameworkCore.Extensions;

namespace Myafim.Infrastructure.Repositories;

public class AccountsRepository(MyafimDbContext context) : IAccountsRepository
{
    public async Task<Pagination<Account>> ListAsync(int page, int limit)
    {
        return await context.Accounts.AsPaginationAsync(page, limit);
    }

    public async Task<long> GetBalanceAsync(int id)
    {
        return await context.Accounts
            .Where(account => account.Id == id)
            .Select(account => account.IncomingTransactions.Sum(transaction => transaction.Amount) -
                               account.OutgoingTransactions.Sum(transaction => transaction.Amount))
            .SingleOrDefaultAsync();
    }
}