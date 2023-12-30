using Myafim.Domain.Models;

namespace Myafim.API.Models;

public record TransactionDto
(
    int Id,
    string? Description,
    long Amount,
    DateTimeOffset ValueDate,
    int SourceAccountId,
    int DestinationAccountId,
    int? CategoryId
)
{
    public static TransactionDto FromDomain(Transaction transaction) =>
        new(transaction.Id,
            transaction.Description,
            transaction.Amount,
            transaction.ValueDate,
            transaction.SourceAccountId,
            transaction.DestinationAccountId,
            transaction.CategoryId
        );
}