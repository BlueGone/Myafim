namespace Myafim.Domain;

public class Transaction
{
    public int Id { get; set; }

    /// <remarks>Is expressed in minor units (e.g. cents).</remarks>
    public long Amount { get; set; }

    public int SourceAccountId { get; set; }
    public Account SourceAccount { get; set; } = null!;

    public int DestinationAccountId { get; set; }
    public Account DestinationAccount { get; set; } = null!;

    public int? CategoryId { get; set; }
    public Category? Category { get; set; } = null!;
}
