using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Myafim.Domain.Handlers;
using Myafim.Domain.Models;
using Pagination.EntityFrameworkCore.Extensions;
using static Microsoft.AspNetCore.Http.TypedResults;

namespace Myafim.API.Endpoints;

public static class CategoriesEndpoints
{
    public static void RegisterCategoriesEndpoints(this WebApplication app)
    {
        var groupBuilder = app.MapGroup("/categories");

        groupBuilder.MapGet("/", ListCategories);
    }

    private static async Task<Ok<Pagination<Category>>> ListCategories(
        [FromServices] ListCategoriesHandler handler,
        int page, int limit)
    {
        return Ok(await handler.HandleAsync(page, limit));
    }
}
