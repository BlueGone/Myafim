using Myafim.Domain.Models;

namespace Myafim.Domain.Requests;

public record CreateTransactionRequest(
    string? Description,
    long Amount,
    DateTimeOffset ValueDate,
    int SourceAccountId,
    int DestinationAccountId,
    int? CategoryId)
    : ICreateTransactionRequest;