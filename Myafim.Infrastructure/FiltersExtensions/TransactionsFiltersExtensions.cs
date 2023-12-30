using Myafim.Domain.Filters;
using Myafim.Domain.Models;

namespace Myafim.Infrastructure.FiltersExtensions;

public static class TransactionsFiltersExtensions
{
    public static IQueryable<Transaction> ApplyFilters(this IQueryable<Transaction> queryable, TransactionsFilters filters)
    {
        return queryable
            .WhereIf(filters.MinValueDate is not null, transaction => transaction.ValueDate >= filters.MinValueDate)
            .WhereIf(filters.MaxValueDate is not null, transaction => transaction.ValueDate < filters.MaxValueDate);
    }
}