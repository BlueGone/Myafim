using Myafim.Domain.Repositories;

namespace Myafim.Domain.Handlers;

public class GetAccountBalanceHandler(IAccountsRepository accountsRepository)
{
    public Task<long> HandleAsync(int id, CancellationToken cancellationToken = default)
    {
        return accountsRepository.GetBalanceAsync(id, cancellationToken);
    }
}