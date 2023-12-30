using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Myafim.Domain.Models;

public class Transaction
{
    [Key]
    public int Id { get; set; }

    [MaxLength(80)]
    public string? Description { get; set; }
    /// <remarks>Is expressed in minor units (e.g. cents).</remarks>
    public long Amount { get; set; }

    public DateTimeOffset ValueDate { get; set; }

    [ForeignKey(nameof(SourceAccount))]
    public int SourceAccountId { get; set; }
    public Account SourceAccount { get; set; } = null!;

    [ForeignKey(nameof(DestinationAccountId))]
    public int DestinationAccountId { get; set; }
    public Account DestinationAccount { get; set; } = null!;

    [ForeignKey(nameof(CategoryId))]
    public int? CategoryId { get; set; }
    public Category? Category { get; set; } = null!;
}
