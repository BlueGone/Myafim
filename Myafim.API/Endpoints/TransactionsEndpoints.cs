using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Myafim.Domain;
using Myafim.Infrastructure;
using static Microsoft.AspNetCore.Http.TypedResults;

namespace Myafim.API.Endpoints;

public static class TransactionsEndpoints
{
    public static void RegisterTransactionsEndpoints(this WebApplication app)
    {
        var groupBuilder = app.MapGroup("/transactions");

        groupBuilder.MapGet("/", GetTransactions);
    }

    private static async Task<Ok<IReadOnlyCollection<Transaction>>> GetTransactions(
        [FromServices] MyafimDbContext dbContext)
    {
        return Ok<IReadOnlyCollection<Transaction>>(await dbContext.Transactions.ToListAsync());
    }
}