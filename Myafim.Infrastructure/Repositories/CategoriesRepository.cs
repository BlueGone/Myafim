using Myafim.Domain.Models;
using Myafim.Domain.Repositories;
using Pagination.EntityFrameworkCore.Extensions;

namespace Myafim.Infrastructure.Repositories;

public class CategoriesRepository(MyafimDbContext context) : ICategoriesRepository
{
    public async Task<Pagination<Category>> ListAsync(int page, int limit, CancellationToken cancellationToken = default)
    {
        return await context.Categories.AsPaginationAsync(page, limit, cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyCollection<Category>> CreateRangeAsync(IReadOnlyCollection<Category> categories, CancellationToken cancellationToken = default)
    {
        await context.Categories.AddRangeAsync(categories, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return categories;
    }
}