using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Myafim.API.Models;
using Myafim.Domain.Handlers;
using static Microsoft.AspNetCore.Http.TypedResults;

namespace Myafim.API.Endpoints;

public static class CategoriesEndpoints
{
    public static void RegisterCategoriesEndpoints(this WebApplication app)
    {
        var groupBuilder = app.MapGroup("/categories");

        groupBuilder.MapGet("/", ListCategories);
    }

    private static async Task<Ok<PaginationDto<CategoryDto>>> ListCategories(
        [FromServices] ListCategoriesHandler handler,
        int page = 1, int limit = 50)
    {
        return Ok(PaginationDto<CategoryDto>.FromDomain(
            await handler.HandleAsync(page, limit),
            CategoryDto.FromDomain));
    }
}
