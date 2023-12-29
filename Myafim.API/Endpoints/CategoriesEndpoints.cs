using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Myafim.Domain;
using Myafim.Infrastructure;
using static Microsoft.AspNetCore.Http.TypedResults;

namespace Myafim.API.Endpoints;

public static class CategoriesEndpoints
{
    public static void RegisterCategoriesEndpoints(this WebApplication app)
    {
        var groupBuilder = app.MapGroup("/categories");

        groupBuilder.MapGet("/", GetCategories);
    }

    private static async Task<Ok<IReadOnlyCollection<Category>>> GetCategories([FromServices] MyafimDbContext dbContext)
    {
        return Ok<IReadOnlyCollection<Category>>(await dbContext.Categories.ToListAsync());
    }
}
