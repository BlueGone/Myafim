using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Myafim.API.Models;
using Myafim.Domain.Filters;
using Myafim.Domain.Handlers;
using Myafim.Domain.Requests;
using static Microsoft.AspNetCore.Http.TypedResults;

namespace Myafim.API.Endpoints;

public static class TransactionsEndpoints
{
    public static void RegisterTransactionsEndpoints(this WebApplication app)
    {
        var groupBuilder = app.MapGroup("/transactions");

        groupBuilder.MapGet("/", ListTransactions);
        groupBuilder.MapGet("/{id}", GetTransactionById).WithName(nameof(GetTransactionById));
        groupBuilder.MapPost("/", CreateTransaction);
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
    
    private static async Task<Results<
        Ok<TransactionDto>,
        NotFound
    >> GetTransactionById(
        [FromServices] GetTransactionByIdHandler handler,
        int id,
        CancellationToken cancellationToken = default)
    {
        var transaction = await handler.HandleAsync(id, cancellationToken);
        return transaction is not null
            ? Ok(TransactionDto.FromDomain(transaction))
            : NotFound();
    }
    
    private static async Task<Results<
        CreatedAtRoute<TransactionDto>,
        BadRequest<List<string>>
    >> CreateTransaction(
        [FromServices] CreateTransactionHandler handler,
        CreateTransactionRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.HandleAsync(request, cancellationToken);
        return result.TryGetRight(out var transaction, out var errors)
            ? CreatedAtRoute(TransactionDto.FromDomain(transaction), nameof(GetTransactionById),
                new { id = transaction.Id })
            : BadRequest(errors.Select(error => error.ToString()).ToList());
    }
}
