using System.Linq.Expressions;

namespace Myafim.Infrastructure;

public static class QueryableExtensions
{
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> queryable, bool condition, Expression<Func<T, bool>> predicate) =>
        condition ? queryable.Where(predicate) : queryable;
}