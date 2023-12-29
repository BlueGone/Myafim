using Myafim.Domain.Models;
using Myafim.Domain.Repositories;
using Pagination.EntityFrameworkCore.Extensions;

namespace Myafim.Infrastructure.Repositories;

public class CategoriesRepository(MyafimDbContext context) : ICategoriesRepository
{
    public async Task<Pagination<Category>> ListAsync(int page, int limit)
    {
        return await context.Categories.AsPaginationAsync(page, limit);
    }

    public async Task<IReadOnlyCollection<Category>> CreateRangeAsync(IReadOnlyCollection<Category> categories)
    {
        await context.Categories.AddRangeAsync(categories);
        await context.SaveChangesAsync();
        return categories;
    }
}