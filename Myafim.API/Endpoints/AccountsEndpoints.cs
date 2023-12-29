using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Myafim.Domain.Handlers;
using Myafim.Domain.Models;
using Pagination.EntityFrameworkCore.Extensions;
using static Microsoft.AspNetCore.Http.TypedResults;

namespace Myafim.API.Endpoints;

public static class AccountsEndpoints
{
    public static void RegisterAccountsEndpoints(this WebApplication app)
    {
        var groupBuilder = app.MapGroup("/accounts");

        groupBuilder.MapGet("/", ListAccounts);
    }

    private static async Task<Ok<Pagination<Account>>> ListAccounts(
        [FromServices] ListAccountsHandler handler,
        int page, int limit)
    {
        return Ok(await handler.HandleAsync(page, limit));
    }
}
