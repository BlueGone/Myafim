using Myafim.Domain.Models;
using Myafim.Domain.Repositories;

namespace Myafim.Domain.Handlers;

public class GetTransactionByIdHandler(ITransactionsRepository transactionsRepository)
{
    public Task<Transaction?> HandleAsync(int id, CancellationToken cancellationToken = default)
    {
        return transactionsRepository.GetByIdAsync(id, cancellationToken);
    }
}