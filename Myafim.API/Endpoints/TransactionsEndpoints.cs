using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Myafim.Domain.Handlers;
using Myafim.Domain.Models;
using Pagination.EntityFrameworkCore.Extensions;
using static Microsoft.AspNetCore.Http.TypedResults;

namespace Myafim.API.Endpoints;

public static class TransactionsEndpoints
{
    public static void RegisterTransactionsEndpoints(this WebApplication app)
    {
        var groupBuilder = app.MapGroup("/transactions");

        groupBuilder.MapGet("/", ListTransactions);
    }

    private static async Task<Ok<Pagination<Transaction>>> ListTransactions(
        [FromServices] ListTransactionsHandler handler,
        int page, int limit)
    {
        return Ok(await handler.HandleAsync(page, limit));
    }
}
