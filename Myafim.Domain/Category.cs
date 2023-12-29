using System.Text;

namespace Myafim.Domain;

public class Category
{
    public int Id { get; set; }

    public required string Name { get; set; }
    public required Rune? Emoji { get; set; }
}
