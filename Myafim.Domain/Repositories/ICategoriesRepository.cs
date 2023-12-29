using Myafim.Domain.Models;
using Pagination.EntityFrameworkCore.Extensions;

namespace Myafim.Domain.Repositories;

public interface ICategoriesRepository
{
    Task<Pagination<Category>> ListAsync(int page, int limit);
    Task<IReadOnlyCollection<Category>> CreateRangeAsync(IReadOnlyCollection<Category> categories);
}