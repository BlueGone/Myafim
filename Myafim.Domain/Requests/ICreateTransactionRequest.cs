namespace Myafim.Domain.Requests;

public interface ICreateTransactionRequest
{
    string? Description { get; }
    /// <remarks>Is expressed in minor units (e.g. cents).</remarks>
    long Amount { get; }
    DateTimeOffset ValueDate { get; }
    int SourceAccountId { get; }
    int DestinationAccountId { get; }
    int? CategoryId { get; }
}