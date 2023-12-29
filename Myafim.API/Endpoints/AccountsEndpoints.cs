using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Myafim.Domain;
using Myafim.Infrastructure;
using Pagination.EntityFrameworkCore.Extensions;
using static Microsoft.AspNetCore.Http.TypedResults;

namespace Myafim.API.Endpoints;

public static class AccountsEndpoints
{
    public static void RegisterAccountsEndpoints(this WebApplication app)
    {
        var groupBuilder = app.MapGroup("/accounts");

        groupBuilder.MapGet("/", GetAccounts);
    }

    private static async Task<Ok<Pagination<Account>>> GetAccounts(
        [FromServices] MyafimDbContext dbContext,
        int page, int limit)
    {
        return Ok(await dbContext.Accounts.AsPaginationAsync(page, limit));
    }
}
