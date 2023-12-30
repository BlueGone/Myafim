using Myafim.Domain.Models;
using Myafim.Domain.Repositories;
using Pagination.EntityFrameworkCore.Extensions;

namespace Myafim.Domain.Handlers;

public class ListCategoriesHandler(ICategoriesRepository categoriesRepository)
{
    public Task<Pagination<Category>> HandleAsync(int page, int limit, CancellationToken cancellationToken = default)
    {
        return categoriesRepository.ListAsync(page, limit, cancellationToken);
    }
}