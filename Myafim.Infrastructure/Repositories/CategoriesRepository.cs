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
}