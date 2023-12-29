using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Myafim.Domain.Models;

public class Account
{
    [Key]
    public int Id { get; set; }

    [MaxLength(80)]
    public required string Name { get; set; }

    [InverseProperty(nameof(Transaction.SourceAccount))]
    public ICollection<Transaction> IncomingTransactions { get; set; } = null!;

    [InverseProperty(nameof(Transaction.DestinationAccount))]
    public ICollection<Transaction> OutgoingTransactions { get; set; } = null!;
}
