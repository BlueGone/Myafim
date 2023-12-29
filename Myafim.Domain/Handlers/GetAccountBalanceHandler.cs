using Myafim.Domain.Repositories;

namespace Myafim.Domain.Handlers;

public class GetAccountBalanceHandler(IAccountsRepository accountsRepository)
{
    public Task<long> HandleAsync(int id)
    {
        return accountsRepository.GetBalanceAsync(id);
    }
}