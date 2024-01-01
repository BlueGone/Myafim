using Myafim.Domain.Filters;
using Myafim.Domain.Models;

namespace Myafim.Infrastructure.FiltersExtensions;

public static class AccountsFiltersExtensions
{
    public static IQueryable<Account> ApplyFilters(this IQueryable<Account> queryable, AccountsFilters filters)
    {
        return queryable
            .WhereIf(filters.Ids is not null, account => filters.Ids!.Contains(account.Id));
    }
}