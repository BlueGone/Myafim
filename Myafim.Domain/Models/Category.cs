using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Myafim.Domain.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [MaxLength(80)]
    public required string Name { get; set; }

    public required Rune? Emoji { get; set; }
    
    [InverseProperty(nameof(Transaction.Category))]
    public List<Transaction> Transactions { get; set; } = null!;
}
