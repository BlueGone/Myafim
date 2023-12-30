using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Myafim.API.Models;
using Myafim.Domain.Filters;
using Myafim.Domain.Handlers;
using static Microsoft.AspNetCore.Http.TypedResults;

namespace Myafim.API.Endpoints;

public static class TransactionsEndpoints
{
    public static void RegisterTransactionsEndpoints(this WebApplication app)
    {
        var groupBuilder = app.MapGroup("/transactions");

        groupBuilder.MapGet("/", ListTransactions);
    }

    private static async Task<Ok<PaginationDto<TransactionDto>>> ListTransactions(
        [FromServices] ListTransactionsHandler handler,
        DateTimeOffset? minValueDate,
        DateTimeOffset? maxValueDate,
        int page = 1, int limit = 50,
        CancellationToken cancellationToken = default)
    {
        var filters = new TransactionsFilters
        {
            MinValueDate = minValueDate,
            MaxValueDate = maxValueDate
        };

        return Ok(PaginationDto<TransactionDto>.FromDomain(
            await handler.HandleAsync(filters, page, limit, cancellationToken),
            TransactionDto.FromDomain));
    }
}
