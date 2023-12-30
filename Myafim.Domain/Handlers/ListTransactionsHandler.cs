using Myafim.Domain.Models;
using Myafim.Domain.Repositories;
using Pagination.EntityFrameworkCore.Extensions;

namespace Myafim.Domain.Handlers;

public class ListTransactionsHandler(ITransactionsRepository transactionsRepository)
{
    public Task<Pagination<Transaction>> HandleAsync(int page, int limit, CancellationToken cancellationToken = default)
    {
        return transactionsRepository.ListAsync(page, limit, cancellationToken);
    }
}