using Myafim.Domain.Filters;
using Myafim.Domain.Models;
using Myafim.Domain.Repositories;
using Pagination.EntityFrameworkCore.Extensions;

namespace Myafim.Domain.Handlers;

public class ListTransactionsHandler(ITransactionsRepository transactionsRepository)
{
    public Task<Pagination<Transaction>> HandleAsync(TransactionsFilters filters, int page, int limit, CancellationToken cancellationToken = default)
    {
        return transactionsRepository.ListAsync(filters, page, limit, cancellationToken);
    }
}