using Myafim.Domain.Models;
using Myafim.Domain.Repositories;
using Pagination.EntityFrameworkCore.Extensions;

namespace Myafim.Domain.Handlers;

public class ListAccountsHandler(IAccountsRepository accountsRepository)
{
    public Task<Pagination<Account>> HandleAsync(int page, int limit, CancellationToken cancellationToken = default)
    {
        return accountsRepository.ListAsync(page, limit, cancellationToken);
    }
}