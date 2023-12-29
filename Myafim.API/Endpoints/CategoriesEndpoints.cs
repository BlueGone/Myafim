using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Myafim.Domain.Models;
using Myafim.Infrastructure;
using Pagination.EntityFrameworkCore.Extensions;
using static Microsoft.AspNetCore.Http.TypedResults;

namespace Myafim.API.Endpoints;

public static class CategoriesEndpoints
{
    public static void RegisterCategoriesEndpoints(this WebApplication app)
    {
        var groupBuilder = app.MapGroup("/categories");

        groupBuilder.MapGet("/", GetCategories);
    }

    private static async Task<Ok<Pagination<Category>>> GetCategories(
        [FromServices] MyafimDbContext dbContext,
        int page, int limit)
    {
        return Ok(await dbContext.Categories.AsPaginationAsync(page, limit));
    }
}
