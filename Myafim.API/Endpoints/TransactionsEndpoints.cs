using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Myafim.Domain.Models;
using Myafim.Infrastructure;
using Pagination.EntityFrameworkCore.Extensions;
using static Microsoft.AspNetCore.Http.TypedResults;

namespace Myafim.API.Endpoints;

public static class TransactionsEndpoints
{
    public static void RegisterTransactionsEndpoints(this WebApplication app)
    {
        var groupBuilder = app.MapGroup("/transactions");

        groupBuilder.MapGet("/", GetTransactions);
    }

    private static async Task<Ok<Pagination<Transaction>>> GetTransactions(
        [FromServices] MyafimDbContext dbContext,
        int page, int limit)
    {
        return Ok(await dbContext.Transactions.AsPaginationAsync(page, limit));
    }
}
